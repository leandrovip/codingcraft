using System;
using System.ComponentModel;
using CodingCraft.Ex01.Models.Interfaces;

namespace CodingCraft.Ex01.Models.EntidadeBase
{
    public class EntidadeNaoEditavel : IEntidadeNaoEditavel
    {
        [DisplayName("Dt. Cadastro")]
        public DateTime DataCadastro { get; set; }

        [DisplayName("Usuário Cad.")]
        public string UsuarioCadastro { get; set; }
    }
}