using EF.CORE.DICASETRUQUES.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.CORE.DICASETRUQUES.Data
{
    public class ApplicationContext :DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb; Initial Catalog=Tips;Integrated Security=true;pooling=true;")
            .EnableSensitiveDataLogging();

        }
    }
}
