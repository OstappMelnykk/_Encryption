using EncryptionWebApplication.Models;
using EncryptionWebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EncryptionWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult DevInfo()
        {
            return View("DevInfo");
        }


    }
}
