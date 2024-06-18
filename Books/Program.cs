using Domain.Entity;
using Infarstuructre.Data;
using Infarstuructre.IRepository;
using Infarstuructre.IRepository.ServicesRepository;
using Infarstuructre.Seeds;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<BookDBContext>(opations =>
        {
            opations.UseSqlServer(builder.Configuration.GetConnectionString("BooksCon"));
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opations =>
        {
            opations.Password.RequiredLength = 5;
            opations.Password.RequireNonAlphanumeric = false;
            opations.Password.RequiredUniqueChars = 0;
            opations.Password.RequireDigit = false;
            opations.Password.RequireLowercase = false;
            opations.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<BookDBContext>();
        builder.Services.AddSession();
        builder.Services.ConfigureApplicationCookie(opation =>
        {
            opation.LoginPath = "/admin";
            opation.AccessDeniedPath = "/admin/home/denied";
        });

        builder.Services.AddScoped<IServicesRepository<Category>, ServicesCategory>();
        builder.Services.AddScoped<IServicesRepositoryLog<CategoryLog>, ServicesLogCategory>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();



        app.UseEndpoints(endpoints =>
        {

            endpoints.MapControllerRoute(
                       name: "areas",
                       pattern: "{area:exists}/{controller=Accounts}/{action=login}/{id?}");
            endpoints.MapControllerRoute(
            name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });



        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManger = services.GetRequiredService<RoleManager<IdentityRole>>();
            await DefaultRoles.SeedAsync(roleManger);
            await DefaultUsers.SeedSuperAdminAsync(userManger, roleManger);
            await DefaultUsers.SeedAdminAsync(userManger, roleManger);
            await DefaultUsers.SeedBasicAsync(userManger, roleManger);
        }
        catch (Exception)
        {

            throw;
        }
        app.Run();
    }
}