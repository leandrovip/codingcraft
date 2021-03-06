﻿using System.ComponentModel.DataAnnotations;

namespace CodingCraftEx04.MVC.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}