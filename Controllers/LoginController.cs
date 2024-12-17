using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System.Net;
using System.Security.Claims;

namespace CoreApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly CoreDataContext _context;
        private readonly CoreDataContextProcedures _contextProcedures;

    public static int TerminationLock;
        public LoginController(CoreDataContext context,CoreDataContextProcedures contextProcedures)
        {  _context = context;
      _contextProcedures = contextProcedures;

    }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
          if (CheckActivited(_contextProcedures) != -99)
          {
            return RedirectToAction("ActivationForm", "Login");
          }

      if (ModelState.IsValid)
          {
        
            var user = _context.Users.Where(x => (x.UserName == model.UserNameOrEmail || x.UserEmail == model.UserNameOrEmail) && x.Password == model.Password).FirstOrDefault();

            if (user != null)
            {
              //success, cresate cookie
              var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.UserEmail),
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

    public IActionResult InternetClose()
    {

      return View();
    }
    public IActionResult ActivationForm()
    {

      return View();
    }
    [HttpPost]
    public IActionResult ActivationForm(LoginViewModel model)
    {
      if (CheckForInternetConnection() == true)
      {
        SqlDataReader sdr;
        string resutmsg;
        resutmsg = "";
        int cresult;
        cresult = 1;
        var data_Customer = _contextProcedures.getdumpdataonlineAsync("GIVEMYCOMPANYDATA").Result.Select(static x => x.result).FirstOrDefault();
        var data_Product = _contextProcedures.getdumpdataonlineAsync("GIVEMYCOMPANYDATA").Result.Select(static x => x.Msg).FirstOrDefault();
        resutmsg = data_Product;
        TempData["msg2"] = resutmsg;
      }
      else
      {
        TempData["msg2"] = "Net nh chal rh ap kay";
      }

      return View();
    }

    public static  int CheckActivited(CoreDataContextProcedures dd)
    {
      //SqlDataReader sdr;
      //string mSQL;
      int ccheck = 0;

      var data = dd.getdumpdataofflineAsync("GIVEMYCOMPANYDATA").Result.Select(static x => x.TerminationLock).FirstOrDefault();
      TerminationLock = Convert.ToInt16(data);

      if (TerminationLock != -99)
      {

        ccheck = TerminationLock;
      };
      return ccheck;
    }
    public static bool CheckForInternetConnection()
    {
      try
      {
        using (var client = new WebClient())
        using (var stream = client.OpenRead("http://www.google.com"))
        {
          return true;
        }
      }
      catch
      {
        return false;
      }
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
