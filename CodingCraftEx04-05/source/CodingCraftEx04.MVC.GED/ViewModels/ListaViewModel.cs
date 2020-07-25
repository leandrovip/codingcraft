using System.Collections.Generic;

namespace CodingCraftEx04.MVC.GED.ViewModels
{
    public class ListaViewModel
    {
        public string TipoArquivo { get; set; }
        public List<ArquivoViewModel> Arquivos { get; set; }
    }
}