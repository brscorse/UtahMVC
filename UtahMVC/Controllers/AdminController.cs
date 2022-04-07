using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UtahMVC.Models;
using UtahMVC.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace UtahMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IUtahMVCRepository repo { get; set; }

        public AdminController(IUtahMVCRepository temp)
        {
            repo = temp;
        }

        public IActionResult Admin(string countyNames, int pageNum = 1)
        {

            int pageSize = 25;
            ViewBag.counties = countyNames;

            // calculate how many rows to show on each page and have page numbers to correspond 
            var x = new CrashesViewModel
            {
               
                UtahCrashData = repo.UtahCrashData
                .Where(c => c.COUNTY_NAME == countyNames || countyNames == null).OrderByDescending(x => x.CRASH_ID)
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


        //details for admin, including edit and delete buttons 
        public IActionResult AdminDetails(string id)
        {
            var crash = repo.UtahCrashData.Single(x => x.CRASH_ID == id);

            return View(crash);
        }

        //edit crash page 
        [HttpGet]
        public IActionResult AdminEdit(string id)
        {
            //Add viewbag for county and city
            var crash = repo.UtahCrashData.Single(x => x.CRASH_ID == id);


            return View("AdminEdit", crash);
        }

        //edit crash post and update
        [HttpPost]
        public IActionResult AdminEdit(Crash c)
        {
            //add id so it goes back to details for that record
            repo.SaveCrash(c);

            return RedirectToAction("Admin");
        }

        //delete confirmation page
        [HttpGet]
        public IActionResult AdminDelete(string id)
        {
            var crash = repo.UtahCrashData.Single(x => x.CRASH_ID == id);

            return View("AdminDelete", crash);
        }

        //delete post
        [HttpPost]
        public IActionResult AdminDelete(Crash c)
        {
            repo.DeleteCrash(c);

            return RedirectToAction("Admin");
        }

        // add crash 
        [HttpGet]
        public IActionResult AdminAdd()
        {
            //Add viewbag for county and city
            ViewBag.Id = repo.UtahCrashData.Max(x => x.CRASH_ID) + 1;
            return View("AdminAdd", new Crash());
        }

        [HttpPost]
        public IActionResult AdminAdd(Crash crash)
        {
            //add it so that after you create it goes to details of the record you just created
            if (ModelState.IsValid)
            {
                repo.CreateCrash(crash);

                return View("AdminDetails", crash);
            }
            else
            {
                return View("AdminAdd", crash);

            }
        }
    }
}
