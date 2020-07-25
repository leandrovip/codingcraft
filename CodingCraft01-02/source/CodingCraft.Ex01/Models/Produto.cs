using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex01.Models.EntidadeBase;

namespace CodingCraft.Ex01.Models
{
    [Table("Produtos")]
    public class Produto : Entidade
    {
        [Key]
        public Guid ProdutoId { get; set; }

        [Required]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("R$ Valor")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [DisplayName("Categoria")]
        public Guid CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}