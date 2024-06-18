using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class UserRoleView
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public bool ActiveUser { get; set; }

    }
}
