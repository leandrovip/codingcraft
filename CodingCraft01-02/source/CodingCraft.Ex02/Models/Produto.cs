using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex02.Models.EntidadeBase;

namespace CodingCraft.Ex02.Models
{
    [Table("Produtos")]
    public class Produto : Entidade
    {
        [Key]
        public Guid ProdutoId { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Tamanho { get; set; }
        public string Cor { get; set; }
        public decimal Estoque { get; set; }

        [DisplayName("R$ Valor")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [DisplayName("Grupo")]
        public Guid GrupoProdutoId { get; set; }

        public virtual GrupoProduto GrupoProduto { get; set; }
    }
}