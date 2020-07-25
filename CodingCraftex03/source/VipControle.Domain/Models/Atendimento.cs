using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VipControle.Domain.Enum;
using VipControle.Domain.Models.Base;

namespace VipControle.Domain.Models
{
    [Table("Atendimentos")]
    public class Atendimento : EntidadeEditavel
    {
        [Key]
        public Guid AtendimentoId { get; set; }

        public Guid ClienteId { get; set; }

        public Guid AtendenteId { get; set; }

        public Guid TipoAtendimentoId { get; set; }

        [Required]
        public string Solicitante { get; set; }

        public Status Status { get; set; }
        public Prioridade Prioridade { get; set; }

        [Display(Name = "Descrição Atendimento")]
        [Required]
        [StringLength(1000)]
        public string DescricaoAtendimento { get; set; }

        [Display(Name = "Descrição Conclusão")]
        [StringLength(1000)]
        public string DescricaoConclusao { get; set; }

        [Display(Name = "Observação")]
        [StringLength(1000)]
        public string Observacao { get; set; }

        [Display(Name = "Data Previsão")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataPrevisao { get; set; }

        [Display(Name = "Data Conclusão")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataConclusao { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Atendente Atendente { get; set; }
        public virtual TipoAtendimento TipoAtendimento { get; set; }
    }
}