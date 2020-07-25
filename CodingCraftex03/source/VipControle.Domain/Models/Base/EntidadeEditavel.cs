using System;
using System.ComponentModel.DataAnnotations;
using VipControle.Domain.Interfaces;

namespace VipControle.Domain.Models.Base
{
    public class EntidadeEditavel : EntidadeNaoEditavel, IEntidadeEditavel
    {
        [Display(Name = "Data Edição")]
        public DateTime? DataEdicao { get; set; }

        [Display(Name="Usuário Edição")]
        public string UsuarioEdicao { get; set; }
    }
}