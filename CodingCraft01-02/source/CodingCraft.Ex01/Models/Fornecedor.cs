using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex01.Models.EntidadeBase;

namespace CodingCraft.Ex01.Models
{
    [Table("Fornecedores")]
    public class Fornecedor:Entidade
    {
        [Key]
        public Guid FornecedorId { get; set; }

        [Required]
        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }

        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }
    }
}