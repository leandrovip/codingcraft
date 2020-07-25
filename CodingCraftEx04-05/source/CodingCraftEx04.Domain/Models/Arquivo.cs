using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CodingCraftEx04.Domain.Models
{
    [Table("Arquivos")]
    public class Arquivo
    {
        [Key]
        public Guid ArquivoId { get; set; }

        [Required(ErrorMessage = "Informe o nome do arquivo")]
        public string Nome { get; set; }

        public string Caminho { get; set; }
        public string Tamanho { get; set; }
        public string TipoDeArquivo { get; set; }
        public DateTime DataUpload { get; set; }

        public Guid UsuarioId { get; set; }
        public string Usuario { get; set; }

        [JsonIgnore]
        public virtual ICollection<ArquivoDetalhe> ArquivoDetalhe { get; set; }
    }
}