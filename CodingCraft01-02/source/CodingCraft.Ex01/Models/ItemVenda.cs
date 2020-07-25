using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex01.Models.EntidadeBase;

namespace CodingCraft.Ex01.Models
{
    [Table("ItensVenda")]
    public class ItemVenda: Entidade
    {
        [Key]
        public Guid ItemVendaId { get; set; }

        [DisplayName("Descrição")]
        public Guid ProdutoId { get; set; }

        public Guid VendaId { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Informa uma quantidade válida entre 1 e 1000.")]
        public int Quantidade { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Informe um valor válido.")]
        [DisplayName("R$ Valor")]
        public decimal Valor { get; set; }

        [DataType(DataType.Currency, ErrorMessage = "Informe um valor válido.")]
        [DisplayName("R$ Total")]
        public decimal ValorTotal { get; set; }

        public virtual Venda Venda { get; set; }
        public virtual Produto Produto { get; set; }
    }
}