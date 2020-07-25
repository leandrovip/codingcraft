using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VipControle.Domain.Models
{
    [Table("CstCofins")]
    public class CstCofins
    {
        [StringLength(2)]
        public string CstCofinsId { get; set; }

        [StringLength(200)]
        public string Descricao { get; set; }

        public int TipoCst { get; set; }
    }
}