using System.ComponentModel.DataAnnotations;

namespace CodingCraftEx04.Api.ViewModels
{
    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha precisa ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "Senhas não conferem, tente novamente.")]
        public string ConfirmPassword { get; set; }
    }
}