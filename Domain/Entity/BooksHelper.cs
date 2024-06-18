using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class BooksHelper
    {
        public static string GroupColor(string name)
        {
            switch(name)
            {
                case "SuperAdmin": return "badge-danger";
                case "Admin": return "badge-success";
                default: return "badge-warning";


            }
        }

        public const string msgType = "msgType";
        public const string title = "title";
        public const string msg = "msg";
        
        public const string getImagesPath = "/images/users/";
        public const string saveImagesPath = "images/users/";


        public const string Save = "Save";
        public const string Update = "Update";
        public const string Delete = "Delete";

        // Data for default user
        public const string Email = "superadmin@domin.com";
        public const string UserName = "superadmin@domin.com";
        public const string Name = "Super Admin";
        public const string Password = "superadmin@password";
        public const string Image = "default.png";

        public const string AdminEmail = "admin@domin.com";
        public const string AdminUserName = "admin@domin.com";
        public const string AdminName = "Admin";
        public const string AdminPassword = "admin@password";

        public const string BasicEmail = "basic@domin.com";
        public const string BasicUserName = "basic@domin.com";
        public const string BasicName = "Basic";
        public const string BasicPassword = "basic@password";
        public const string Permission = "Permission";



        public enum eCurrentState
        {
            Active = 1,
            Delete=0
        }

        public enum Roles
        {
            SuperAdmin,
            Admin,
            Basic
        }

        public enum PermissionModuleName
        {
            Accounts,
            Roles,
            Registers,
            Categories,
            Home
        }
    }
}
