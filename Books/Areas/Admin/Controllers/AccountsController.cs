using Domain.Entity;
using Infarstuructre.Data;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Books.Resource;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountsController : Controller
    {
        #region Declaration
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly BookDBContext context;
        private readonly SignInManager<ApplicationUser> signInManager;
        #endregion

        #region Constructor
        public AccountsController(
           RoleManager<IdentityRole> roleManager,
           UserManager<ApplicationUser> userManager, BookDBContext context,
           SignInManager<ApplicationUser> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
            this.signInManager = signInManager;
        }
        #endregion

        void SessionData(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(BooksHelper.msgType, msgType);
            HttpContext.Session.SetString(BooksHelper.title, title);
            HttpContext.Session.SetString(BooksHelper.msg, msg);
        }
        #region Roles Actions
        public IActionResult Roles()
        {
            return View
                (
                 new RolesVM()
                 {
                     NewRole = new NewRole(),
                     Roles = roleManager.Roles.OrderBy(r => r.Name).ToList()
                 });
        }
        [Authorize(Roles = "Admin")]
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Roles(NewRole newRole)
        {
            //if (ModelState.IsValid)
            //{
            IdentityRole role = new IdentityRole()
            {
                Name = newRole.Name,
                Id = newRole.Id
            };

            // create new role
            if (role.Id == null)
            {
                role.Id = Guid.NewGuid().ToString();
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    SessionData
                        (
                        Resource.Resource.successType,
                        Resource.Resource.saveMsgTitle,
                        Resource.Resource.saveMsgContent
                        );
                    return RedirectToAction("Roles");
                }
                else
                {
                    SessionData
                     (
                     Resource.Resource.errorType,
                     Resource.Resource.errorMsgTitle,
                     Resource.Resource.errorMsgContent
                     );
                
                    return RedirectToAction("Roles");
                }
            }
            // update  role
            else
            {
                IdentityRole editRole = await roleManager.FindByIdAsync(role.Id);
                editRole.Name = role.Name;
                editRole.Id = role.Id;

                var result = await roleManager.UpdateAsync(editRole);
                if (result.Succeeded)
                {
                    SessionData
                    (
                    Resource.Resource.successType,
                    Resource.Resource.updateMsgTitle,
                    Resource.Resource.updateMsgContent
                    );
                   
                    return RedirectToAction("Roles");
                }
                else
                {
                    SessionData
                   (
                   Resource.Resource.errorType,
                   Resource.Resource.updateMsgTitleError,
                   Resource.Resource.updateMsgContentError
                   );
                  
                    return RedirectToAction("Roles");
                }
            }

            // }
            return View();

        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = roleManager.Roles.FirstOrDefault(r => r.Id == id);
            if (role != null)
            {
                await roleManager.DeleteAsync(role);
            }
            return RedirectToAction(nameof(Roles));

        }



        #endregion

       

        #region User Login Actions
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var result = await signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index) , "Home");
            }

            ViewBag.LoginError = false;

            return View(loginVM);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        } 
        #endregion


       
        #region Register Actions
    
        public IActionResult Register()
        {

            return View(new RegsiterVM()
            {
                NewRegsiter = new NewRegsiter(),
                Users = context.UserRoleViews.ToList(),
                Roles = roleManager.Roles.ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = "Admin,Basic")]
      
        public async Task<IActionResult> Register(RegsiterVM model)
        {
            //   if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(files[0].FileName);
                    var fileStream = new FileStream(Path.Combine(@"wwwroot/", BooksHelper.saveImagesPath, ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);
                    model.NewRegsiter.ImageUser = ImageName;
                }
                else
                {
                    model.NewRegsiter.ImageUser = model.NewRegsiter.ImageUser;
                }
                var user = new ApplicationUser
                {

                    Id = model.NewRegsiter.Id,
                    Name = model.NewRegsiter.Name,
                    Email = model.NewRegsiter.Email,
                    UserName = model.NewRegsiter.Email,
                    ImageUser = model.NewRegsiter.ImageUser,
                    ActiveUser = model.NewRegsiter.ActiveUser,

                };
                if (user.Id == null)
                {
                    // Create New User
                    user.Id = Guid.NewGuid().ToString();
                    var result = await userManager.CreateAsync(user, model.NewRegsiter.Password);
                    if (result.Succeeded)
                    {
                        //Succeeded
                        var res = await userManager.AddToRoleAsync(user, model.NewRegsiter.RoleName);
                        if (res.Succeeded)

                            SessionData
                                (
                                Resource.Resource.successType,
                                Resource.Resource.saveMsgTitle,
                                Resource.Resource.saveMsgContent
                                );

                        else

                            // failed
                            SessionData
                                (
                                Resource.Resource.errorType,
                                Resource.Resource.errorMsgTitle,
                                Resource.Resource.errorMsgContent
                                );

                    }

                    else
                    {
                        // failed
                        SessionData
                                (
                                Resource.Resource.errorType,
                                Resource.Resource.errorMsgTitle,
                                Resource.Resource.errorMsgContent
                                );
                        return RedirectToAction(nameof(Register));

                    }

                }
                else
                {
                    //Updata User
                    var updateUser = await userManager.FindByIdAsync(user.Id);
                    updateUser.Id = user.Id;
                    updateUser.Name = user.Name;
                    updateUser.UserName = user.Email;
                    updateUser.Email = user.Email;
                    updateUser.ImageUser = user.ImageUser;
                    updateUser.ActiveUser = user.ActiveUser;
                    var result = await userManager.UpdateAsync(updateUser);
                    if (result.Succeeded)
                    {
                        var oldRole = await userManager.GetRolesAsync(updateUser);
                        if (oldRole != null)
                        {
                            await userManager.RemoveFromRolesAsync(updateUser, oldRole);

                        }

                        if ((await userManager.AddToRoleAsync(updateUser, model.NewRegsiter.RoleName)).Succeeded)
                        {
                                SessionData
                                      (
                                      Resource.Resource.successType,
                                      Resource.Resource.updateMsgTitle,
                                      Resource.Resource.updateMsgContent
                                      );
                            }
                        else
                        {
                                SessionData
                                    (
                                    Resource.Resource.errorType,
                                    Resource.Resource.updateMsgTitleError,
                                    Resource.Resource.updateMsgContentError
                                    );
                            }

                    }
                    else
                    {
                        HttpContext.Session.SetString("msgType", "success");
                        HttpContext.Session.SetString("title", "لم يتم التعديل");
                        HttpContext.Session.SetString("msg", "لم يتم تعديل  بيانات المستخدم   ");
                    }

                }
                return RedirectToAction("Register", "Accounts");
            }
            return RedirectToAction("Register", "Accounts");

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.ImageUser))
                {
                    var path = Path.Combine("wwwroot", BooksHelper.saveImagesPath, user.ImageUser);
                    if(System.IO.File.Exists(path))
                     System.IO.File.Delete(path);
                }
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Register));

        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PasswordRecovery(RegsiterVM model)
        {
            var user = await userManager.FindByIdAsync(model.PasswordRecoveryVM.Id);
            if (user != null)
            {
                await userManager.RemovePasswordAsync(user);
                var resutl = await userManager.AddPasswordAsync(user, model.PasswordRecoveryVM.NewPassword);

                if (resutl.Succeeded)
                {
                    HttpContext.Session.SetString("msgType", "success");
                    HttpContext.Session.SetString("title", "تم التعديل");
                    HttpContext.Session.SetString("msg", "تم تغير كلمة المرور بنجاح");
                }
                else
                {
                    // failed
                    HttpContext.Session.SetString("msgType", "error");
                    HttpContext.Session.SetString("title", "فشل التعديل");
                    HttpContext.Session.SetString("msg", "لم يتم تغير كلمة المرور "); ;

                }

            }

            return RedirectToAction(nameof(Register));
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return View(nameof(Login));
        }
        #endregion

      
    }
}
