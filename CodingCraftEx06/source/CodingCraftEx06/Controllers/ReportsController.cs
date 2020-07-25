using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodingCraftEx06.Models;
using CodingCraftEx06.Models.Context;
using CodingCraftEx06.ViewModels;
using Dapper;

namespace CodingCraftEx06.Controllers
{
    public class ReportsController : Controller
    {
        private readonly FloodContext db = new FloodContext();

        // GET: Analçytics
        public async Task<ActionResult> Analytical()
        {
            var countries = await db.Database.Connection
                .QueryAsync<Country>("SELECT CountryId, Name FROM Countries ORDER BY Name");

            ViewBag.CountryId = new SelectList(countries, "CountryId", "Name");

            return View();
        }

        //Post
        [HttpPost]
        public async Task<ActionResult> Analytical(Guid CountryId, int firstYear = 1970, int secoundYear = 2008)
        {
            #region Countries

            var countries = await db.Database.Connection
                .QueryAsync<Country>("SELECT CountryId, Name FROM Countries ORDER BY Name");

            ViewBag.CountryId = new SelectList(countries, "CountryId", "Name");

            #endregion

            var sql = "SELECT Year, Amount FROM FloodKillings " +
                      "WHERE CountryId = @CountryId AND Year BETWEEN @First AND @Secound " +
                      "ORDER BY Year";

            var param = new DynamicParameters();
            param.Add("CountryId", CountryId, DbType.Guid);
            param.Add("First", firstYear, DbType.Int32);
            param.Add("Secound", secoundYear, DbType.Int32);

            var analytical = await db.Database.Connection.QueryAsync(sql, param);

            ViewBag.SumAmount = analytical.Sum(x => x.Amount);

            return View(analytical);
        }

        // Get: Aggregate
        public async Task<ActionResult> Aggregate()
        {
            var countries = await db.Database.Connection
                .QueryAsync<Country>("SELECT CountryId, Name FROM Countries ORDER BY Name");

            ViewBag.CountryId = new SelectList(countries, "CountryId", "Name");

            return View();
        }

        //Post: Aggregate
        [HttpPost]
        public async Task<ActionResult> Aggregate(Guid CountryId, int firstYear = 1970, int secoundYear = 2008)
        {
            #region Countries

            var countries = await db.Database.Connection
                .QueryAsync<Country>("SELECT CountryId, Name FROM Countries ORDER BY Name");

            ViewBag.CountryId = new SelectList(countries, "CountryId", "Name");

            #endregion

            const string sql =
                "SELECT AVG(Amount) as DeathAverage, SUM(Amount) as DeathAmount, Count(Amount) as YearsCount, " +
                "MAX(Amount) as DeathMax, MIN(Amount) as DeathMin, STDEV(Amount) as StandardDeviation " +
                "FROM FloodKillings " +
                "WHERE CountryId = @CountryId AND Year BETWEEN @First AND @Secound";

            var param = new DynamicParameters();
            param.Add("CountryId", CountryId, DbType.Guid);
            param.Add("First", firstYear, DbType.Int32);
            param.Add("Secound", secoundYear, DbType.Int32);

            // Dapper
            var result = await db.Database.Connection.QueryFirstOrDefaultAsync<AggregateReport>(sql, param);

            result.CountryName = countries.FirstOrDefault(x => x.CountryId == CountryId).Name;
            result.FirstYear = firstYear.ToString();
            result.SecoundYear = secoundYear.ToString();

            return View(result);
        }

        // Get:
        public ActionResult Chart()
        {
            return View();
        }

        //Post:
        [HttpPost]
        public async Task<ActionResult> ChartPost(int firstYear = 1970, int secoundYear = 2008)
        {
            const string sql = "SELECT SUM(Amount) as FloodKillingData, [Year] as YearLabel " +
                               "FROM FloodKillings " +
                               "WHERE Year BETWEEN @First AND @Secound " +
                               "GROUP BY [Year] " +
                               "ORDER BY [Year]";

            var param = new DynamicParameters();
            param.Add("First", firstYear, DbType.Int32);
            param.Add("Secound", secoundYear, DbType.Int32);

            var result = await db.Database.Connection.QueryAsync<ChartViewModel>(sql, param);

            return View("Chart", result);
        }
    }
}