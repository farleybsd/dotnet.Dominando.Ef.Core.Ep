using System;
using System.Linq;
using Transacoes.Data;
using Transacoes.Domain;

namespace Transacoes
{
    class Program
    {
        static void Main(string[] args)
        {
            ComportamentoPadrao();
            Console.ReadKey();
        }

        static void ComportamentoPadrao()
        {
            CadastrarLivro();

            using (var db = new ApplicationContext())
            {
                var livro = db.Livros.FirstOrDefault(p=> p.Id==1);
                livro.Autor = "Rafael Almeida";

                db.Livros.Add(
                    new Livro
                    {
                        Titulo = "Dominando o Entity Framework Core",
                        Autor = "Rafael Almeida"
                    }
                );

                db.SaveChanges();
            }
        }
        static void CadastrarLivro()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Livros.Add(
                    new Livro
                    {
                        Titulo="Introdução Ao Entity Framework Core",
                        Autor="Rafael"
                    }
                    );

                db.SaveChanges();
            }
        }
    }
}
