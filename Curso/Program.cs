using DominandoEfCore.Data;
using System;

namespace DominandoEfCore
{
    class Program
    {
        static void Main(string[] args)
        {
            EnsureDeleted();
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
    }
}
