using HabilbitandoLog.EF.CORE.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace HabilbitandoLog.EF.CORE.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DevIo-02;Integrated Security=True;pooling=true";

            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine,LogLevel.Information); // coletando os log do ef no console
                
                
               
        }

    }
}
