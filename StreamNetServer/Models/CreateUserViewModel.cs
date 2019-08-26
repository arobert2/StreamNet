using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamNetServer.Models
{
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string PasswordCheck { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
