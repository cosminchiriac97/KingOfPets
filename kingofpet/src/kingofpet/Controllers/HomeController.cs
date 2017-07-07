using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace kingofpet.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Admin")]
        public IActionResult Admin()
        {
            ViewData["Message"] = "Your admin page!";
            //Response.Cookies["userName"].Value = "patrick";
            //Response.Cookies["userName"].Expires = DateTime.Now.AddDays(1);

            //Response.Cookies.Add(aCookie);
            return View("~/Views/Home/Admin.cshtml");
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
