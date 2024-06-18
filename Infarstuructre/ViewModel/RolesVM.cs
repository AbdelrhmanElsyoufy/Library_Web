using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Resources;
namespace Infarstuructre.ViewModel
{
    public class RolesVM
    {
        public NewRole NewRole { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }

    public class NewRole
    {
        
        public string Id { get; set; } = null;
        [Required(ErrorMessageResourceType =(typeof(ResourceData)) , ErrorMessageResourceName ="RoleName")]
        public string Name { get; set; }
    }



}
