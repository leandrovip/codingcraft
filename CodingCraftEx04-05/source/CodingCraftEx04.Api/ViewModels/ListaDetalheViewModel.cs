using System;
using System.Xml.Serialization;

namespace CodingCraftEx04.Api.ViewModels
{
    [XmlType(TypeName = "ListaDetalhe")]
    public class ListaDetalheViewModel
    {
        public Guid ArquivoDetalheId { get; set; }
        public Guid ArquivoId { get; set; }
        public DateTime DataDownload { get; set; }
        public string Usuario { get; set; }
    }
}