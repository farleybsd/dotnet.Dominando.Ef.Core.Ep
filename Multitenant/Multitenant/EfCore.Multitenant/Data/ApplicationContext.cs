using EfCore.Multitenant.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Multitenant.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {

        }
    }
}
