using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ViewModel.Entities;

namespace faraboom.Controllers {
    public class LoginController : Controller {
        public static string eror, massage, NewFileName;
        private readonly ContextHampadco db;
        private readonly IHostingEnvironment _env;

        public LoginController (ContextHampadco _db, IHostingEnvironment env) {
            db = _db;
            _env = env;

        }

        public IActionResult Index () {

            return View ();
        }
        public IActionResult Login () {
            if (eror != null) {
                // ViewBag.eror = eror;
                eror = null;

            }

            if (massage != null) {
                ViewBag.msg = massage;
                massage = null;

            }
            return View ();
        }
        public IActionResult Register () {

            return View ();
        }
        public IActionResult RegisterAgency () {

            return View ();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////AddRegisterAgency

        public async Task<IActionResult> AddRegisterAgency (Vm_Register VmReg) {

            //  if (db.Tbl_Registers.Any (a => a.CodeMeli == VmReg.CodeMeli)) {
            //     massage = "اطلاعات فردی با این کد ملی قبلا ثبت شده است";
            //     return RedirectToAction ("Login");

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////upload file
            string FileExtension1 = Path.GetExtension (VmReg.NameFile.FileName);
            NewFileName = String.Concat (Guid.NewGuid ().ToString (), FileExtension1);
            var path = $"{_env.WebRootPath}\\fileupload\\{NewFileName}";
            using (var stream = new FileStream (path, FileMode.Create)) {
                await VmReg.NameFile.CopyToAsync (stream);
            }
            //////////////////////////end upload file 

            Tbl_Register TblReg = new Tbl_Register () {
                NameFamily = VmReg.NameFamily,
                CodeMeli = VmReg.CodeMeli,
                NumberPhone = VmReg.NumberPhone,
                Password = VmReg.Password,
                Email = VmReg.Email,
                Address = VmReg.Address,
                UploadFile = NewFileName

            };
            db.Tbl_Registers.Add (TblReg);
            db.SaveChanges ();
            massage = "اطلاعات شما با موفقیت ثبت شد لطفا ایمیل خود را چک فرمایید و منتظر تایید از سوی مذیریت باشید";
            return RedirectToAction ("Login");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////AddRegisterAgency

        public IActionResult ForgotPass () {
            return View ();
        }
        public IActionResult login_check (Vm_Users us) {

            var user = db.Tbl_Users.Where (a => a.Username == us.Username && a.Password == us.Password).SingleOrDefault ();

            if (user != null) {

                var claims = new List<Claim> () {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, "admin")
                };

                var identity = new ClaimsIdentity (claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal (identity);

                var properties = new AuthenticationProperties {
                    IsPersistent = true
                };

                HttpContext.SignInAsync (principal, properties);
                return RedirectToAction ("Index", "Home", new { area = "admin" });

            } else {
                eror = "نام کاربری یا رمز عبور شما نادرست است";
                return RedirectToAction ("Login");
            }

        }
    }
}