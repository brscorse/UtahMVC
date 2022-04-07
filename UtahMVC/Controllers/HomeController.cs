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




        public IActionResult Crashes(string countyNames, string severity, int pageNum = 1)
        {

            int pageSize = 10;

            // calculate how many rows to show on each page and have page numbers to correspond

            // if severity ISN'T passed in
            if (severity == null)
            {
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

            // if a county name isn't passed in
            if (countyNames == null)
            {
                var x = new CrashesViewModel
                {
                    UtahCrashData = repo.UtahCrashData
                    .Where(c => c.CRASH_SEVERITY_ID == severity || severity == null)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),


                    PageInfo = new PageInfo
                    {
                        TotalNumCrashes =
                            (severity == null
                                ? repo.UtahCrashData.Count()
                                : repo.UtahCrashData.Where(x => x.CRASH_SEVERITY_ID == severity).Count()),
                        CrashesPerPage = pageSize,
                        CurrentPage = pageNum
                    }
                };
                return View(x);
            }

            // if both county and severity is passed in
            if (countyNames != null && severity != null)
            {
                var x = new CrashesViewModel
                {
                    UtahCrashData = repo.UtahCrashData
                    .Where(c => c.CRASH_SEVERITY_ID == severity && c.COUNTY_NAME == countyNames)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize),


                    PageInfo = new PageInfo
                    {
                        TotalNumCrashes =
                            (severity == null
                                ? repo.UtahCrashData.Count()
                                : repo.UtahCrashData.Where(x => x.CRASH_SEVERITY_ID == severity && x.COUNTY_NAME == countyNames).Count()),
                        CrashesPerPage = pageSize,
                        CurrentPage = pageNum
                    }
                };
                return View(x);
            }



            return View();
            
        }

        public IActionResult Details(string id)
        {
            var crash = repo.UtahCrashData.Single(x => x.CRASH_ID == id);

            return View(crash);
        }

        

    }
}
