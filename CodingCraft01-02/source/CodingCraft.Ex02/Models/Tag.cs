using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex02.Models.EntidadeBase;

namespace CodingCraft.Ex02.Models
{
    [Table("Tags")]
    public class Tag : Entidade
    {
        [Key]
        public Guid TagId { get; set; }

        [Required]
        public string Nome { get; set; }

        public virtual ICollection<GrupoProdutoTag> GrupoProdutoTags { get; set; }
    }
}