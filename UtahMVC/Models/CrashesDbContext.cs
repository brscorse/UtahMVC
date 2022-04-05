using System;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace UtahMVC.Models
{
    public class CrashesDbContext : DbContext
    {

        public CrashesDbContext(DbContextOptions<CrashesDbContext> options) : base(options)
        {

        }

        public DbSet<Crash> UtahCrashData { get; set; }

    }
}