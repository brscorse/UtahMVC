using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UtahMVC.Models;

namespace UtahMVC.Component
{
    public class FilterViewComponent : ViewComponent
    {
        private CrashesDbContext context { get; set; }

        public FilterViewComponent(CrashesDbContext temp)
        {
            context = temp;
        }

        // function to grab the distinct different categories to display on the home page
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["countyNames"];
            var counties = context.UtahCrashData.Select(x => x.COUNTY_NAME).Distinct().OrderBy(x => x);
            ViewBag.severity = context.UtahCrashData.Select(x => x.CRASH_SEVERITY_ID).Distinct().OrderBy(x => x);
            ViewBag.year = context.UtahCrashData.Select(x => x.CRASH_YEAR).Distinct().OrderBy(x => x);

            return View(counties);
        }
    }
}