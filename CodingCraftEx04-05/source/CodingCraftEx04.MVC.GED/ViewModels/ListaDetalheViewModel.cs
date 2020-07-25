using System;

namespace CodingCraftEx04.MVC.GED.ViewModels
{
    public class ListaDetalheViewModel
    {
        public Guid ArquivoDetalheId { get; set; }
        public Guid ArquivoId { get; set; }
        public DateTime DataDownload { get; set; }
        public string Usuario { get; set; }
    }
}