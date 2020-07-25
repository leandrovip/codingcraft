using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VipControle.Domain.Models.Base;

namespace VipControle.Domain.Models
{
    [Table("Atendentes")]
    public class Atendente : EntidadeEditavel
    {
        [Key]
        public Guid AtendenteId { get; set; }

        [Required(ErrorMessage = "Nome é requerido")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail é requerido")]
        [StringLength(254)]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public virtual ICollection<Atendimento> Atendimentos { get; set; }
    }
}