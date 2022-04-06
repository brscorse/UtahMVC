using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtahMVC.Models
{
    public class EFUtahMVCRepository : IUtahMVCRepository
    {
        private CrashesDbContext context { get; set; }

        public EFUtahMVCRepository(CrashesDbContext temp)
        {
            context = temp;
        }

        public IQueryable<Crash> UtahCrashData => context.UtahCrashData;

        

        public void SaveCrash(Crash c)
        {
            context.SaveChanges();
        }

        public void CreateCrash(Crash c)
        {
            context.Add(c);
            context.SaveChanges();

        }

        public void DeleteCrash(Crash c)
        {
            context.Remove(c);
            context.SaveChanges();
        }


        //public IQueryable<Crash> Crashes => throw new NotImplementedException();
    }
}
