using System;
using System.ComponentModel;
using CodingCraft.Ex02.Models.Interfaces;

namespace CodingCraft.Ex02.Models.EntidadeBase
{
    public class Entidade : EntidadeNaoEditavel, IEntidade
    {
        [DisplayName("Dt. Edição")]
        public DateTime? DataEdicao { get; set; }

        [DisplayName("Usu. Edição")]
        public string UsuarioEdicao { get; set; }
    }
}