using Domain.Entity;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.Data
{
    public class BookDBContext : IdentityDbContext<ApplicationUser>
    {
        public BookDBContext(DbContextOptions<BookDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>().ToTable("Users", schema: "Identity");
            builder.Entity<IdentityRole>().ToTable("Roles", schema: "Identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", schema: "Identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", schema: "Identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", schema: "Identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", schema: "Identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", schema: "Identity");
            //set default value for id
            builder.Entity<Category>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<CategoryLog>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<SubCategory>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<SubCategoryLog>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<Book>().Property(x => x.Id).HasDefaultValueSql("(newid())");
            builder.Entity<BookLog>().Property(x => x.Id).HasDefaultValueSql("(newid())");

            //configure view
            builder.Entity<UserRoleView>().HasNoKey().ToView("UserRoleView");


            
           
            
        }

            public DbSet<Category> Categories { get; set; }
            public DbSet<CategoryLog> CategoryLogs { get; set; }
            public DbSet<SubCategory> SubCategories { get; set; }
            public DbSet<SubCategoryLog> SubCategoryLogs { get; set; }
            public DbSet<Book> Books { get; set; }
            public DbSet<BookLog> BookLogs { get; set; }
            public DbSet<UserRoleView> UserRoleViews { get; set; }

    

    }
}
