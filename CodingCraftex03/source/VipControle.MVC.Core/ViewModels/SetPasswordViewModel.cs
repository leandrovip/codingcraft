using System.ComponentModel.DataAnnotations;

namespace VipControle.MVC.Core.ViewModels
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("NewPassword", ErrorMessage = "Senha não confere.")]
        public string ConfirmPassword { get; set; }
    }
}