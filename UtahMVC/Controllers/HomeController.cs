using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private IUtahMVCRepository repo { get; set; }
        //private Prediction _context { get; set; }

        public HomeController(IUtahMVCRepository temp)
        {
            repo = temp;
            //_context = cont;
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
            //ViewBag.Score = _context.PredictedValue;

            return View();
        }


        // records page
        public IActionResult Crashes(string countyNames, int pageNum = 1)
        {
            int pageSize = 10;
            var x = new CrashesViewModel
            {
                UtahCrashData = repo.UtahCrashData
                .Where(c => c.COUNTY_NAME == countyNames || countyNames == null)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),


                PageInfo = new PageInfo
                {
                    TotalNumCrashes =
                        (countyNames == null
                            ? repo.UtahCrashData.Count()
                            : repo.UtahCrashData.Where(x => x.COUNTY_NAME == countyNames).Count()),
                    CrashesPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            return View(x);
   
        }

        public IActionResult Details(string id)
        {
            var crash = repo.UtahCrashData.Single(x => x.CRASH_ID == id);

            return View(crash);
        }

        

    }
}
