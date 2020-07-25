using System.ComponentModel.DataAnnotations;

namespace CodingCraftEx04.Api.ViewModels
{
    // Models used as parameters to AccountController actions.

    public class SetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "A senha precisa ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("NewPassword", ErrorMessage = "Senhas não conferem, tente novamente.")]
        public string ConfirmPassword { get; set; }
    }
}