using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using VipControle.Domain.Models.Base;

namespace VipControle.Domain.Models
{
    [Table("Clientes")]
    public class Cliente : EntidadeEditavel
    {
        [Key]
        public Guid ClienteId { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        [StringLength(100)]
        public string RazaoSocial { get; set; }

        [Required]
        [Display(Name = "Fantasia")]
        [StringLength(50)]
        public string Fantasia { get; set; }

        [Required]
        [Display(Name = "CPF / CNPJ")]
        [StringLength(20)]
        public string CpfCnpj { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [StringLength(20)]
        public string Celular { get; set; }

        [Display(Name = "Nome do Contato")]
        [StringLength(50)]
        public string NomeContato { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(254)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Logradouro { get; set; }

        [StringLength(20)]
        public string Numero { get; set; }

        [StringLength(50)]
        public string Bairro { get; set; }

        [StringLength(50)]
        public string Cidade { get; set; }

        [StringLength(30)]
        public string Estado { get; set; }

        [StringLength(20)]
        public string Cep { get; set; }

        [StringLength(200)]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public virtual ICollection<Atendimento> Atendimentos { get; set; }
    }
}