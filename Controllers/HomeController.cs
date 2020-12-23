﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using faraboom.Models;

namespace faraboom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public IActionResult Gallery()
        {
            return View();
        }
        
        public IActionResult AboutUs()
        {
            return View();
        }
        
        public IActionResult Blog()
        {
            return View();
        }
        
        public IActionResult ContactUs()
        {
            return View();
        }
        
        public IActionResult Faq()
        {
            return View();
        }
        
        public IActionResult Ourteam()
        {
            return View();
        }
    }
}
