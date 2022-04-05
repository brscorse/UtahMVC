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
            ViewBag.SelectedType = RouteData?.Values["cityNames"];

            var cities = context.UtahCrashData.Select(x => x.CITY).Distinct().OrderBy(x => x);
            return View(cities);
        }
    }
}