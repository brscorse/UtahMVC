using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
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


        public IActionResult Crashes(string cityNames, int pageNum = 1)
        {

            int pageSize = 25;

            // calculate how many rows to show on each page and have page numbers to correspond 
            var x = new CrashesViewModel
            {
                Crashes = context.UtahCrashData
                .Where(c => c.CITY == cityNames || cityNames == null)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),


                PageInfo = new PageInfo
                {
                    TotalNumCrashes =
                        (cityNames == null
                            ? context.UtahCrashData.Count()
                            : context.UtahCrashData.Where(x => x.CITY == cityNames).Count()),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

        public IActionResult Details(string id)
        {
            var crash = context.UtahCrashData.Single(x => x.CRASH_ID == id);

            return View(crash);
        }

    }
}
