using Domain.Entity;
using Infarstuructre.IRepository;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Books.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IServicesRepository<Category> _servicesCategory;
        private readonly IServicesRepositoryLog<CategoryLog> _servicesCategoryLog;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoriesController(IServicesRepository<Category> servicesCategory,
            IServicesRepositoryLog<CategoryLog> servicesCategoryLog,
            UserManager<ApplicationUser> userManager)
        {
            _servicesCategory = servicesCategory;
            _servicesCategoryLog = servicesCategoryLog;
            _userManager = userManager;
        }
        public IActionResult Categories()
        {
            return View(new CategoryVM
            {
                Categories = _servicesCategory.GetAll(),
                LogCategories = _servicesCategoryLog.GetAll(),
                NewCategory = new Category()
            });
        }

        public  IActionResult Delete(Guid Id)
        {
            // get current user
            var userId = _userManager.GetUserId(User);
            if (_servicesCategory.Delete(Id) && _servicesCategoryLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction(nameof(Categories));
            return RedirectToAction(nameof(Categories));
        }
        public IActionResult DeleteLog(Guid Id)
        {
            _servicesCategoryLog.DeleteLog(Id);
            return RedirectToAction(nameof(Categories));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.NewCategory.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Save new category

                    //check if this category is exitit or not
                    if (_servicesCategory.FindBy(model.NewCategory.Name) != null)
                        SessionMsg(
                            Resource.Resource.errorType,
                            Resource.Resource.errorMsgTitle, 
                            Resource.Resource.saveMsgContentDublict);
                    else
                    {
                        if (_servicesCategory.Save(model.NewCategory)
                            && _servicesCategoryLog.Save(model.NewCategory.Id, Guid.Parse(userId)))
                            SessionMsg(
                                         Resource.Resource.successType,
                                         Resource.Resource.saveMsgTitle,
                                         Resource.Resource.saveMsgContent);
                        else
                            SessionMsg(
                                Resource.Resource.errorType,
                                Resource.Resource.errorMsgTitle,
                                Resource.Resource.errorMsgContent);
                    }
                }
                else
                { //Update
                    if (_servicesCategory.Save(model.NewCategory)
                            && _servicesCategoryLog.Update(model.NewCategory.Id, Guid.Parse(userId)))
                        SessionMsg(Resource.Resource.successType, Resource.Resource.updateMsgTitle, Resource.Resource.updateMsgContent);
                    else
                        SessionMsg(Resource.Resource.errorType, Resource.Resource.updateMsgTitleError, Resource.Resource.updateMsgContentError);
                }
            }
            return RedirectToAction(nameof(Categories));

        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(BooksHelper.msgType, MsgType);
            HttpContext.Session.SetString(BooksHelper.title, Title);
            HttpContext.Session.SetString(BooksHelper.msg, Msg);
        }
    }
}
