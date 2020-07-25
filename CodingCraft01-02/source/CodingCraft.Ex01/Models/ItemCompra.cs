using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex01.Models.EntidadeBase;

namespace CodingCraft.Ex01.Models
{
    [Table("ItensCompra")]
    public class ItemCompra : Entidade
    {
        [Key]
        public Guid ItemCompraId { get; set; }

        public Guid CompraId { get; set; }

        [DisplayName("Produto")]
        public Guid ProdutoId { get; set; }

        [Range(1, 1000, ErrorMessage = "Informe quantidade entre 1 e 1000.")]
        public int Quantidade { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("R$ Unitário")]
        public decimal Valor { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("R$ Total")]
        public decimal ValorTotal { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Compra Compra { get; set; }
    }
}