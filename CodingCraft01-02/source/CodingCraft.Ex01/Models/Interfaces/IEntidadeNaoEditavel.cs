using System;

namespace CodingCraft.Ex01.Models.Interfaces
{
    public interface IEntidadeNaoEditavel
    {
        DateTime DataCadastro { get; set; }
        string UsuarioCadastro { get; set; }
    }
}