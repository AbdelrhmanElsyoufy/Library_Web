using Domain.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class PasswordRecoveryVM
    {
        public string Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterPasswored")]
        [MaxLength(30, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength")]
        [MinLength(5, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLength")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterConfirmPasswored")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterConfirmPassworedُError")]
        [MaxLength(30, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength")]
        [MinLength(5, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLength")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
