using System.ComponentModel.DataAnnotations;

namespace VipControle.MVC.Core.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}