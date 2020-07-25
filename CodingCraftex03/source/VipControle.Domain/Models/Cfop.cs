using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VipControle.Domain.Models
{
    [Table("Cfop")]
    public class Cfop
    {
        [Key]
        public int CfopId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descricao { get; set; }

        [Required]
        public int TipoCfop { get; set; }

        [Required]
        public int DestinoOperacao { get; set; }
    }
}