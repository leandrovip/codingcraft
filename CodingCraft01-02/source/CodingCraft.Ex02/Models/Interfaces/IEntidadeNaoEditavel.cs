using System;

namespace CodingCraft.Ex02.Models.Interfaces
{
    public interface IEntidadeNaoEditavel
    {
        DateTime DataCadastro { get; set; } 
        string UsuarioCadastro { get; set; }
    }
}