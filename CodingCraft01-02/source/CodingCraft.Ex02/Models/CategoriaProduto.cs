using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex02.Models.EntidadeBase;

namespace CodingCraft.Ex02.Models
{
    [Table("Categorias")]
    [DisplayColumn("Nome")]
    public class CategoriaProduto : Entidade
    {
        [Key]
        public Guid CategoriaProdutoId { get; set; }

        [Required]
        public string Nome { get; set; }

        public virtual ICollection<GrupoProduto> GrupoProdutos { get; set; }
    }
}