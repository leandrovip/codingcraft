using System;

namespace CodingCraft.Ex01.Models.Interfaces
{
    public interface IEntidade : IEntidadeNaoEditavel
    {
        DateTime? DataEdicao { get; set; }
        string UsuarioEdicao { get; set; }
    }
}