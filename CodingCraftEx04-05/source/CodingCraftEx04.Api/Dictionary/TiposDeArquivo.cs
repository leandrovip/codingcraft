using System.Collections.Generic;
using CodingCraftEx04.Domain.Models.Enum;

namespace CodingCraftEx04.Api.Dictionary
{
    public static class TipoDeArquivo 
    {
        public static string BuscarNome(string tipoArquivo)
        {
            var tiposDeArquivo = new Dictionary<string, TipoDeArquivoEnum>
            {
                {"jpg", TipoDeArquivoEnum.Imagem},
                {"bmp", TipoDeArquivoEnum.Imagem},
                {"jpeg", TipoDeArquivoEnum.Imagem},
                {"png", TipoDeArquivoEnum.Imagem},
                {"gif", TipoDeArquivoEnum.Imagem},

                {"mp3", TipoDeArquivoEnum.Musica},
                {"wav", TipoDeArquivoEnum.Musica},
                {"wma", TipoDeArquivoEnum.Musica},
                {"ogg", TipoDeArquivoEnum.Musica},

                {"avi", TipoDeArquivoEnum.Video },
                {"mp4", TipoDeArquivoEnum.Video },
                {"flv", TipoDeArquivoEnum.Video },
                {"wmv", TipoDeArquivoEnum.Video },
                {"mpeg", TipoDeArquivoEnum.Video },

                {"txt", TipoDeArquivoEnum.Texto },
                {"csv", TipoDeArquivoEnum.Texto },

                {"doc", TipoDeArquivoEnum.Word },
                {"docx", TipoDeArquivoEnum.Word },
                {"odt", TipoDeArquivoEnum.Word },

                {"xls", TipoDeArquivoEnum.Excel },
                {"xlsx", TipoDeArquivoEnum.Excel },
                {"ods", TipoDeArquivoEnum.Excel },

                {"pps", TipoDeArquivoEnum.PowerPoint },
                {"ppsx", TipoDeArquivoEnum.PowerPoint },
                {"ppt", TipoDeArquivoEnum.PowerPoint },
            };

            return tiposDeArquivo.TryGetValue(tipoArquivo.ToLower(), out var nomePasta) ? nomePasta.ToString() : TipoDeArquivoEnum.Outros.ToString();
        }
    }
}