using System;
using System.ComponentModel;
using CodingCraft.Ex02.Models.Interfaces;

namespace CodingCraft.Ex02.Models.EntidadeBase
{
    public class EntidadeNaoEditavel : IEntidadeNaoEditavel
    {
        [DisplayName("Dt. Cadastro")]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Usu. Cadastro")]
        public string UsuarioCadastro { get; set; }
    }
}