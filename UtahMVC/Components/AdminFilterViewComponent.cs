using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UtahMVC.Models;

namespace UtahMVC.Components
{
    public class AdminFilterViewComponent : ViewComponent
    {
        private CrashesDbContext context { get; set; }

        public AdminFilterViewComponent(CrashesDbContext temp)
        {
            context = temp;
        }

        // function to grab the distinct different categories to display on the home page
        public IViewComponentResult Invoke()
        {
            //ViewBag.SelectedType = RouteData?.Values["cityNames"];
            ViewBag.SelectedType = RouteData?.Values["countyNames"];

            //var cities = context.UtahCrashData.Select(x => x.CITY).Distinct().OrderBy(x => x);
            var counties = context.UtahCrashData.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x);

            return View(counties);
        }
    }
}
