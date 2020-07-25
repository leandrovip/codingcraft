using System.ComponentModel.DataAnnotations;

namespace VipControle.Domain.Enum
{
    public enum Status
    {
        Aberto,
        [Display(Name = "Em Andamento")]
        EmAndamento,
        [Display(Name = "Concluído")]
        Concluido
    }
}