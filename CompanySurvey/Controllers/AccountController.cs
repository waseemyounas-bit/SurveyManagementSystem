using CompanySurvey.Models;
using DataAccess.Data;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace CompanySurvey.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext db;
        private readonly IUOW uOW;
        public AccountController(DataContext dataContext,IUOW _uOW)
        {
            db = dataContext;
            uOW = _uOW;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = uOW.UserRepository().GetAll().Where(x => x.Email == login.Email && x.Password == login.Password && x.RoleId == 1).FirstOrDefault();
                if (user == null)
                {
                    ViewBag.error = "Invalid email or password.";
                    return View(login);
                }
                else
                {
                    HttpContext.Session.SetString("UserId",user.Id.ToString());
                    HttpContext.Session.SetInt32("RoleId",user.RoleId);
                    if(user.RoleId==1)
                       return RedirectToAction("Index","Home");
                    if(user.RoleId==2)
                       return RedirectToAction("Index","Manager");
                }
            }
            return View(login);
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}
