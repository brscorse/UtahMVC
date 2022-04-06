using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtahMVC.Models
{
    public interface IUtahMVCRepository
    {
        IQueryable<Crash> UtahCrashData { get;}

        public void SaveCrash(Crash c);
        public void CreateCrash(Crash c);
        public void DeleteCrash(Crash c);
    }
}
