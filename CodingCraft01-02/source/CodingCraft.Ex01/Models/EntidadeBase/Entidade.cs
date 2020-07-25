using System;
using System.ComponentModel;
using CodingCraft.Ex01.Models.Interfaces;

namespace CodingCraft.Ex01.Models.EntidadeBase
{
    public class Entidade : EntidadeNaoEditavel, IEntidade
    {
        [DisplayName("Data Edição")]
        public DateTime? DataEdicao { get; set; }

        [DisplayName("Usuário Edição")]
        public string UsuarioEdicao { get; set; }
    }
}