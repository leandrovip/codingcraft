using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex01.Models.EntidadeBase;

namespace CodingCraft.Ex01.Models
{
    [Table("Compras")]
    public class Compra : Entidade
    {
        [Key]
        public Guid CompraId { get; set; }

        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Informe uma data válida.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Dt. Compra")]
        [Required]
        public DateTime DataCompra { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Informe uma data válida.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Dt. Pagamento")]
        [Required]
        public DateTime DataPagamento { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("R$ Total")]
        public decimal ValorTotalCompra { get; set; }

        public virtual ICollection<ItemCompra> ItensCompra { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}