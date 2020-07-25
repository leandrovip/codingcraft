using System.ComponentModel.DataAnnotations;

namespace VipControle.MVC.Core.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Telefone")]
        public string Number { get; set; }
    }
}