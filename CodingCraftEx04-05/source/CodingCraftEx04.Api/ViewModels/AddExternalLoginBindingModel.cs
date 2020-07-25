using System.ComponentModel.DataAnnotations;

namespace CodingCraftEx04.Api.ViewModels
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "Token de acesso externo")]
        public string ExternalAccessToken { get; set; }
    }
}