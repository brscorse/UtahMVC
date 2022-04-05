using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using UtahMVC.Models;
using UtahMVC.Models.ViewModels;

namespace UtahMVC.Controllers
{
    public class AdminController : Controller
    {
        private IUtahMVCRepository repo;

        public AdminController(IUtahMVCRepository temp)
        {
            repo = temp;
        }
        
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Details(string id)
        {
            var crash = repo.Crash.Single(x => x.CRASH_ID == id);

            return View(crash);
        }


    }
}
