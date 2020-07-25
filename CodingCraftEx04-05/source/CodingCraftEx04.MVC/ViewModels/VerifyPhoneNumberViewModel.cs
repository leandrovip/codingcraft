using System.ComponentModel.DataAnnotations;

namespace CodingCraftEx04.MVC.ViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }
    }
}