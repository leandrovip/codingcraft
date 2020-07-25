using System;

namespace CodingCraftEx04.MVC.GED.ViewModels
{
    public class ArquivoViewModel
    {
        public Guid ArquivoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataUpload { get; set; }
    }
}