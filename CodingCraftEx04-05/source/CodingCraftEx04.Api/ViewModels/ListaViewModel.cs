using System.Collections.Generic;
using System.Xml.Serialization;

namespace CodingCraftEx04.Api.ViewModels
{
    [XmlType(TypeName = "Lista")]
    public class ListaViewModel
    {
        public string TipoArquivo { get; set; }
        public List<ArquivoViewModel> Arquivos { get; set; }
    }
}