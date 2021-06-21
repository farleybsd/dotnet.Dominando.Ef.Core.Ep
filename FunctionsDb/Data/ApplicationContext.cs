using FunctionsDb.Domain;
using FunctionsDb.FuncoesDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FunctionsDb.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data source=(localdb)\\mssqllocaldb; Initial Catalog=DevIO-02;Integrated Security=true;pooling=true;";

            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MinhasFunçoes.RegistrarFuncoes(modelBuilder);
            modelBuilder.HasDbFunction(typeof(MinhasFunçoes).GetRuntimeMethod("Left",
              new[] { typeof(string), typeof(int) }))
                .HasName("LEFT")
                .IsBuiltIn();
        }
        //[DbFunction(name:"LEFT",schema:"",IsBuiltIn =true)] // funcao incorporada do db funcao nativa
        //public static string Left(string dados,int quantidade)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
