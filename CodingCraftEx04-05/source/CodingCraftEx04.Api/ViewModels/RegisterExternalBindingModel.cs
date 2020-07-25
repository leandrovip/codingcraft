using System.ComponentModel.DataAnnotations;

namespace CodingCraftEx04.Api.ViewModels
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}