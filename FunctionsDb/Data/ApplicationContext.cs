using FunctionsDb.Domain;
using FunctionsDb.FuncoesDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

            modelBuilder
                .HasDbFunction(_letrasMaisculas)
                .HasName("ConverterParaLetrasMaisculas") //nome da funcao no banco de dados
                .HasSchema("dbo");

            modelBuilder
               .HasDbFunction(_dateDiff)
               .HasName("DATEDIFF") //nome da funcao no banco de dados
               .HasTranslation(p=>
               {
                   // remover comportamento do c# para o banco
                   // esse exemplo vamos remover as '' porque no banco da erro
                   var argumentos = p.ToList();

                   var constante = (SqlConstantExpression)argumentos[0];

                   argumentos[0] = new  SqlFragmentExpression(constante.Value.ToString());

                   return new SqlFunctionExpression("DATEDIFF",argumentos,false,new[] {false,false,false },typeof(int),null);
               })
               .IsBuiltIn();
        }

        private static MethodInfo _letrasMaisculas = typeof(MinhasFunçoes)
                       .GetRuntimeMethod(nameof(MinhasFunçoes.LetrasMaiusculas), new[] { typeof(string) });

        private static MethodInfo _dateDiff = typeof(MinhasFunçoes)
                .GetRuntimeMethod(nameof(MinhasFunçoes.DateDiff), new[] { typeof(string), typeof(DateTime), typeof(DateTime) });

        //[DbFunction(name:"LEFT",schema:"",IsBuiltIn =true)] // funcao incorporada do db funcao nativa
        //public static string Left(string dados,int quantidade)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
