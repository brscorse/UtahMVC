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


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Analytics()
        {
            return View();
        }


        public IActionResult Crashes(string countyNames, int pageNum = 1)
        {

            int pageSize = 25;

            // calculate how many rows to show on each page and have page numbers to correspond 
            var x = new CrashesViewModel
            {
                UtahCrashData = context.UtahCrashData
                .Where(c => c.COUNTY_NAME == countyNames || countyNames == null)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),


                PageInfo = new PageInfo
                {
                    TotalNumCrashes =
                        (countyNames == null
                            ? context.UtahCrashData.Count()
                            : context.UtahCrashData.Where(x => x.COUNTY_NAME == countyNames).Count()),
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
