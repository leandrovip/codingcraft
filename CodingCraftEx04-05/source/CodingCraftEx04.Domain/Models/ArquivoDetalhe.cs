using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CodingCraftEx04.Domain.Models
{
    [Table("ArquivosDetalhes")]
    public class ArquivoDetalhe
    {
        [Key]
        public Guid ArquivoDetalheId { get; set; }

        public Guid ArquivoId { get; set; }
        public DateTime DataDownload { get; set; }
        public Guid UsuarioId { get; set; }
        public string Usuario { get; set; }

        [JsonIgnore]
        [ForeignKey("ArquivoId")]
        public virtual Arquivo Arquivo { get; set; }
    }
}