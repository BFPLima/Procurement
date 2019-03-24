using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Procurement.WebAPP.Models
{
    public class UserRegistreViewModel
    {

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public virtual string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm")]
        public virtual string PasswordConfirm { get; set; }


        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public virtual string Email { get; set; }

        [Required]
        [Display(Name = "EmailConfirm")]
        [EmailAddress]
        public virtual string EmailConfirm { get; set; }

       
        public virtual Model.Enums.UserType UserType { get; set; }

        

    }
}