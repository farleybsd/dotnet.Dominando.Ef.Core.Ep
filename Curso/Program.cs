using DominandoEfCore.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace DominandoEfCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //EnsureDeleted();
            GapEnsureCreated();
            Console.ReadKey();
        }
        // Se o banco nao exister ele cria
        static void EnsureCreatedAndDelete() 
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();
        }

        // Deleta todo o banco
        static void EnsureDeleted()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
        }

        //Criando tabela de um outro contexto em um outro banco
        static void GapEnsureCreated()
        {
            using var db1 = new ApplicationContext();
            using var db2 = new ApplicationContextCidade();

            db1.Database.EnsureCreated();
            db2.Database.EnsureCreated();

            var databaseCretador = db2.GetService<IRelationalDatabaseCreator>();
            databaseCretador.CreateTables();
        }
    }
}
