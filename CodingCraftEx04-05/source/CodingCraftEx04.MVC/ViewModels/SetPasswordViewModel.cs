using System.ComponentModel.DataAnnotations;

namespace CodingCraftEx04.MVC.ViewModels
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "A senha precisa ter no m�nimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        [Compare("NewPassword", ErrorMessage = "Senha e confirma��o n�o conferem. Tente novamente.")]
        public string ConfirmPassword { get; set; }
    }
}