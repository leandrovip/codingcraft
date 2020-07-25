using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex01.Models.EntidadeBase;

namespace CodingCraft.Ex01.Models
{
    [Table("Venda")]
    public class Venda : Entidade
    {
        [Key]
        public Guid VendaId { get; set; }

        [DisplayName("Cliente")]
        public Guid ClienteId { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Informe uma data válida.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyy}")]
        [DisplayName("Dt. Venda")]
        [Required]
        public DateTime DataVenda { get; set; }

        [DisplayName("R$ Venda")]
        public decimal ValorVenda { get; set; }

        public virtual ICollection<ItemVenda> ItensVenda { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}