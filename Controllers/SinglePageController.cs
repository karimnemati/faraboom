using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using faraboom.Models;

namespace faraboom.Controllers
{
    public class SinglePageController : Controller
    {

       

        public IActionResult Index1()
        {
            return View();
        }


     public IActionResult Index2()
        {
            return View();
        }
             public IActionResult Index3()
        {
            return View();
        }




        

    }
}
