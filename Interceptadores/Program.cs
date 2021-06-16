using Interceptadores.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Interceptadores
{
    class Program
    {
        static void Main(string[] args)
        {
            //TesteInterceptacao();
            TesteInterceptacaoSaveChanges();
            Console.ReadKey();
        }

        static void TesteInterceptacao()
        {
            using (var db = new ApplicationContext())
            {
                var consulta = db
                                .Funcoes
                                .TagWith("Use NOLOCK")
                                .FirstOrDefault();

                Console.WriteLine($"Consulta:{consulta?.Descricao1}");
            }
        }
        static void TesteInterceptacaoSaveChanges()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Funcoes.Add(
                    new Domain.Funcao
                    {
                        Descricao1 = "Teste"
                    }
                    );

                db.SaveChanges();
            }
        }
    }
}
