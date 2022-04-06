using System;
using System.Linq;

namespace UtahMVC.Models.ViewModels
{
    public class CrashesViewModel
    {
        public IQueryable<Crash> UtahCrashData { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
