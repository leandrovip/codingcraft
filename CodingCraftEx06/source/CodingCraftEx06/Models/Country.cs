using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodingCraftEx06.Models
{
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<FloodKilling> FloodKillings { get; set; }
    }
}