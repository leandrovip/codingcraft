using System.ComponentModel.DataAnnotations;

namespace VipControle.MVC.Core.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}