using System;
using System.ComponentModel.DataAnnotations;

namespace VipControle.Domain.Interfaces
{
    public interface IEntidadeNaoEditavel
    {
        [Display(Name = "Data Cadastro")]
        DateTime DataCadastro { get; set; }

        [Display(Name = "Usuário Cadastro")]
        string UsuarioCadastro { get; set; }
    }
}