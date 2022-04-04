using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UtahMVC.Models;
using UtahMVC.Models.ViewModels;

namespace UtahMVC.Controllers
{
    public class HomeController : Controller
    {

        private CrashesDbContext context { get; set; }

        public HomeController(CrashesDbContext temp)
        {
            context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Crashes(int pageNum = 1)
        {
            int pageSize = 10;

            // calculate how many books to show on each page and have page numbers to correspond 
            var x = new CrashesViewModel
            {
                Crashes = context.Crashes
                .Where(c => c.CITY == "FAUST")
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumCrashes = context.Crashes.Count(),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

        public IActionResult Details(int id)
        {
            var crash = context.Crashes.Single(x => x.CRASH_ID == id);

            return View(crash);
        }

    }
}
