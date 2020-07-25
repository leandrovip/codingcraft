using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VipControle.Domain.Models.Base;

namespace VipControle.Domain.Models
{
    [Table("TiposAtendimento")]
    public class TipoAtendimento : EntidadeEditavel
    {
        [Key]
        public Guid TipoAtendimentoId { get; set; }

        [Required(ErrorMessage = "Descrição é requerida")]
        [StringLength(100)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}