using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VipControle.Domain.Models
{
    [Table("CstIpi")]
    public class CstIpi
    {
        [StringLength(2)]
        public string CstIpiId { get; set; }

        [StringLength(200)]
        public string Descricao { get; set; }

        public int TipoCst { get; set; }
    }
}