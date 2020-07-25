using System;

namespace VipControle.Domain.Interfaces
{
    public interface IEntidadeEditavel : IEntidadeNaoEditavel
    {
        DateTime? DataEdicao { get; set; }
        string UsuarioEdicao { get; set; }
    }
}