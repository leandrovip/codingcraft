using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftEx07.Mvc.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public Guid ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
    }
}