using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftEx06.Models
{
    [Table("FloodKillings")]
    public class FloodKilling
    {
        [Key]
        public Guid FloodKillingId { get; set; }

        [Index("IUQ_FloodKilling_Country", IsUnique = true, Order = 1)]
        public Guid CountryId { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        [Index("IUQ_FloodKilling_Country", IsUnique = true, Order = 2)]
        public int Year { get; set; }

        public virtual Country Country { get; set; }
    }
}