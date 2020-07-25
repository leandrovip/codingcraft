using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraft.Ex01.Models.EntidadeBase;

namespace CodingCraft.Ex01.Models
{
    [Table("Clientes")]
    public class Cliente : Entidade
    {
        [Key]
        public Guid ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }

        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }
    }
}