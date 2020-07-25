using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftEx04.Domain.Models
{
    [Table("UsuarioEndereco")]
    public class UsuarioEndereco
    {
        public Guid UsuarioEnderecoId { get; set; }
        public Guid UsuarioId { get; set; }

        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; } 
    }
}