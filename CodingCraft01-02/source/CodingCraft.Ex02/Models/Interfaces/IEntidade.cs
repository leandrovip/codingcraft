using System;

namespace CodingCraft.Ex02.Models.Interfaces
{
    public interface IEntidade : IEntidadeNaoEditavel
    {
        DateTime? DataEdicao { get; set; }
        string UsuarioEdicao { get; set; }
    }
}