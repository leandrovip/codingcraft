using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VipControle.Domain.Models
{
    [Table("CstPis")]
    public class CstPis
    {
        [StringLength(2)]
        public string CstPisId { get; set; }

        [StringLength(200)]
        public string Descricao { get; set; }

        public int TipoCst { get; set; }
    }
}