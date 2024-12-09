using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CoreApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly CoreDataContext _context;
        public LoginController(CoreDataContext context)
        {  _context = context; }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(x => (x.UserName == model.UserNameOrEmail || x.UserEmail == model.UserNameOrEmail) && x.Password == model.Password).FirstOrDefault();

                if (user != null)
                {
                    //success, cresate cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserEmail),
                        new Claim("Name", user.UserName),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                     return RedirectToAction("Index", "Dashboards");
        }
                else
                {
                    ModelState.AddModelError("", "Username/Email or Password is not correct.");
                }
            }
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(User model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Users.Add(model);
                    _context.SaveChanges();
                    ModelState.Clear();

                    ViewBag.Message = $"{model.UserName} registered successfully. Please Login";
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Please enter unique Email or Password.");
                    return View(model);
                }

                return View();
            }
            return View(model);
        }
        public IActionResult LogOut() 
        { 
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

    }
}
