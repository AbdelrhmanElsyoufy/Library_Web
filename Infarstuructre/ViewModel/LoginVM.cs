using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterEmail")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterEmailError")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterPasswored")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    
        public bool RememberMe { get; set; }
    }
}
