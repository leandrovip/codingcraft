using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CodingCraftEx04.Domain.Models.Enum;

namespace CodingCraftEx04.MVC.ViewModels
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }

        public IEnumerable<string> Permissoes { get; set; }
    }
}