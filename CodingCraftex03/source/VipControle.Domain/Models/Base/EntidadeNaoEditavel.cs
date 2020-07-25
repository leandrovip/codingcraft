using System;
using System.ComponentModel.DataAnnotations;
using VipControle.Domain.Interfaces;

namespace VipControle.Domain.Models.Base
{
    public class EntidadeNaoEditavel : IEntidadeNaoEditavel
    {
        [Display(Name = "Data Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Usuário Cadastro")]
        public string UsuarioCadastro { get; set; }
    }
}