using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using ViewModel.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

using System.Security.Claims;


namespace farboom.Controllers {
    public class LoginController : Controller {
        public static string eror;
        private readonly ContextHampadco db;
        public LoginController (ContextHampadco _db) {
            db = _db;

        }

        public IActionResult Index () {

            return View ();
        }
        public IActionResult Login () {
            if (eror!=null)
            {
                ViewBag.eror=eror;
                eror=null;
                
            }
            return View ();
        }
        public IActionResult Register () {
            
            return View ();
        }
        public IActionResult RegisterAgency () {
            return View ();
        }

        public IActionResult ForgotPass () {
            return View ();
        }
        public IActionResult login_check (Vm_Users us) {

            var user = db.Tbl_Users.Where (a => a.Username == us.Username && a.Password == us.Password).SingleOrDefault ();

            if (user != null) {

                 var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,"admin")
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    HttpContext.SignInAsync(principal, properties);
                    return RedirectToAction("Index", "Home", new { area = "admin" });
                

                
            } else {
                eror = "نام کاربری یا رمز عبور شما نادرست است";
                return RedirectToAction ("Login");
            }

        }
    }
}