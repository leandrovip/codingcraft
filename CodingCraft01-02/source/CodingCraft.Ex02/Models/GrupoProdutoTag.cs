using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex02.Models.EntidadeBase;

namespace CodingCraft.Ex02.Models
{
    [Table("GruposTags")]
    public class GrupoProdutoTag : EntidadeNaoEditavel
    {
        [Key]
        public Guid GrupoProdutoTagId { get; set; }

        public Guid GrupoProdutoId { get; set; }
        public Guid TagId { get; set; }
        public virtual GrupoProduto GrupoProduto { get; set; }
        public virtual Tag Tag { get; set; }
    }
}