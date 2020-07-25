using System;
using System.Xml.Serialization;

namespace CodingCraftEx04.Api.ViewModels
{
    [XmlType(TypeName = "Arquivo")]
    public class ArquivoViewModel
    {
        public Guid ArquivoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataUpload { get; set; }
    }
}