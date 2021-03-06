using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Transacoes.Domain;

namespace Transacoes.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Livro>Livros  { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevIO-02;Integrated Security=true;pooling=true;";

            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
    }
}
