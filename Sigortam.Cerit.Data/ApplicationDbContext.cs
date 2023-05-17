using Microsoft.EntityFrameworkCore;
using Sigortam.Cerit.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigortam.Cerit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             :base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<InsuranceCompany> InsuranceCompany { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
    }
}
