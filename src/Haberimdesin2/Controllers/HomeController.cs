using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Haberimdesin2.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Haber Sizsiniz Bir Sivil Toplum Kuruluşudur";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Bizimle İletişime Geçin";

            return View();
        }
        public IActionResult AddNews()
        {
            ViewData["Message"] = "Publish a News";
            return View();
        }
        public IActionResult EditNews()
        {
            ViewData["Message"] = "Edit a News";
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
