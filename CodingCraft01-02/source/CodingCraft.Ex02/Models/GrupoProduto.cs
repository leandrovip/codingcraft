using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex02.Models.EntidadeBase;

namespace CodingCraft.Ex02.Models
{
    [Table("Grupos")]
    [DisplayColumn("Nome")]
    public class GrupoProduto : Entidade
    {
        [Key]
        public Guid GrupoProdutoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [DisplayName("Categoria")]
        public Guid CategoriaProdutoId { get; set; }

        public virtual CategoriaProduto CategoriaProduto { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
        public virtual ICollection<GrupoProdutoTag> GrupoProdutoTags { get; set; }
    }
}