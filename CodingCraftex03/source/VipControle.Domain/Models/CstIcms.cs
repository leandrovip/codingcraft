using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VipControle.Domain.Models
{
  [Table("CstIcms")]
    public class CstIcms
    {
        [StringLength(3)]
        public string CstIcmsId { get; set; }
        [StringLength(200)]
        public string Descricao { get; set; }
    }
}