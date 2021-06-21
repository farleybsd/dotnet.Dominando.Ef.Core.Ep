using FunctionsDb.Data;
using System;
using System.Linq;

namespace FunctionsDb
{
    class Program
    {
        static void Main(string[] args)
        {
            FuncaoLeft();

            Console.ReadKey();
        }

        static void FuncaoLeft()
        {
            //Built-In Function
            // funcao incorporada do db funcao nativa
            CadastrarLivro();

            using var db = new ApplicationContext();

            var resultado = db.Livros.Select(p=> ApplicationContext.Left(p.Titulo,10));

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
                        Autor = "Rafael"
                    });

                db.SaveChanges();
            }
        }
    }
}
