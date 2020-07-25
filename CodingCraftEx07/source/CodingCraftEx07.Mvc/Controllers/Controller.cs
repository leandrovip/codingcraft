using System.Web.Routing;
using CodingCraftEx07.Mvc.Context;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace CodingCraftEx07.Mvc.Controllers
{
    public abstract class Controller : System.Web.Mvc.Controller
    {
        private NewtonsoftSerializer serializer;
        public StackExchangeRedisCacheClient _redisCacheClient { get; set; }
        protected CodingContext _db;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            #region EF

            _db = new CodingContext();

            #endregion

            #region Redis

            serializer = new NewtonsoftSerializer();

            var configuration = new ConfigurationOptions();
            configuration.EndPoints.Add("localhost", 6379);
            configuration.ConnectTimeout = 200000;
            configuration.SyncTimeout = 200000;
            configuration.AllowAdmin = true;
            _redisCacheClient =
                new StackExchangeRedisCacheClient(ConnectionMultiplexer.Connect(configuration), serializer);

            #endregion
        }
    }
}