using DominandoEfCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominandoEfCore.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C002;Integrated Security=True;polling=true";
            optionsBuilder
                .UseSqlServer(strConnection)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine,LogLevel.Information);
        }
    }
}
