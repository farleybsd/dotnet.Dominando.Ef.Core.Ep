using Dotnet.EfCore.Consultas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Dotnet.EfCore.Consultas.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DevIo-02;Integrated Security=True;pooling=true";
            optionsBuilder
                .UseSqlServer(strConnection,p=>p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)) // evitar explosao cartesiana
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Filtro global em uma tabela
            //modelBuilder.Entity<Departamento>().HasQueryFilter(p => !p.Excluido);
        }
    }
}
