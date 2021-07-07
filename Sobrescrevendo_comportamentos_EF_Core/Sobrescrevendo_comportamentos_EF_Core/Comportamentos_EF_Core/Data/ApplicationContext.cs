using Comportamentos_EF_Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comportamentos_EF_Core.Data
{
    public class ApplicationContext :DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                    .LogTo(Console.WriteLine)
                    .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=sobrescrevendo_comportamento; Integrated Security=true")
                    .EnableSensitiveDataLogging();
        }
    }
}
