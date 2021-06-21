using FunctionsDb.Data;
using FunctionsDb.FuncoesDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FunctionsDb
{
    class Program
    {
        static void Main(string[] args)
        {
            //FuncaoLeft();
            //FuncaoDefinidaPeloUsuario();
            DateDiff();
            Console.ReadKey();
        }

        static void FuncaoLeft()
        {
            //Built-In Function
            // funcao incorporada do db funcao nativa
            CadastrarLivro();

            using var db = new ApplicationContext();

            var resultado = db.Livros.Select(p=> MinhasFunçoes.Left(p.Titulo,10));

            foreach (var parteTitulo in resultado)
            {
                Console.WriteLine(parteTitulo);
            }
        }
        static void CadastrarLivro()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Livros.Add(
                    new Domain.Livro
                    {
                        Titulo = "Introdução ao Entity Framework Core",
                        Autor = "Rafael",
                        CadastradoEm = DateTime.Now.AddDays(-1)
                    });

                db.SaveChanges();
            }
        }
        static void FuncaoDefinidaPeloUsuario()
        {
            CadastrarLivro();

            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw(@"
                CREATE FUNCTION ConverterParaLetrasMaisculas(@dados VARCHAR(100))
                RETURNS VARCHAR(100)
                BEGIN
                    RETURN UPPER(@dados)
                END");

            var resultado = db.Livros.Select(p => MinhasFunçoes.LetrasMaiusculas(p.Titulo));

            foreach (var parteTitulo in resultado)
            {
                Console.WriteLine(parteTitulo);
            }
        }
        static void DateDiff()
        {
            CadastrarLivro();

            using var db = new ApplicationContext();

            /*
            var resultado = db
                            .Livros
                            .Select(p => EF.Functions.DateDiffDay(p.CadastradoEm, DateTime.Now));
            */

            var resultado = db
                            .Livros
                            .Select(p => MinhasFunçoes.DateDiff("DAY",p.CadastradoEm, DateTime.Now));

            foreach (var diff in resultado)
            {
                Console.WriteLine(diff);
            }
        }
    }
}
