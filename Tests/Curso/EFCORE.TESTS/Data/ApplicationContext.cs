using EFCORE.TESTS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCORE.TESTS.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {

        }
    }
}
