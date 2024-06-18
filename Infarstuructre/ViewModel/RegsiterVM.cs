using Domain.Entity;
using Domain.Resources;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class RegsiterVM 
    {
        public NewRegsiter NewRegsiter { get; set; }
        public PasswordRecoveryVM PasswordRecoveryVM { get; set; }
        public List<UserRoleView> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
    public class NewRegsiter
    {
        public string Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterName")]
        [MaxLength(30, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength")]
        [MinLength(3, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLength")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RoleName")]
        public string RoleName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterEmail")]
        [EmailAddress(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterEmailError")]
        public string Email { get; set; }
        public string? ImageUser { get; set; }
        public bool ActiveUser { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterPasswored")]
        [MaxLength(30, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength")]
        [MinLength(3, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterConfirmPasswored")]
        [Compare ("Password" ,ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "RegisterConfirmPassworedُError")]
        [MaxLength(30, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MaxLength")]
        [MinLength(3, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "MinLength")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


    }
}
