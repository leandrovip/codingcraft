using System.ComponentModel.DataAnnotations;

namespace VipControle.MVC.Core.ViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Número Telefone")]
        public string PhoneNumber { get; set; }
    }
}