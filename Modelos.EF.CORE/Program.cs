using Microsoft.EntityFrameworkCore;
using Modelos.EF.CORE.Data;
using System;

namespace Modelos.EF.CORE
{
    class Program
    {
        static void Main(string[] args)
        {
            //Collations();
            PropagarDados();
            Console.ReadKey();
        }

        static void Collations()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        static void PropagarDados()
        {
            // Criar a tabela ja com dados 
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }
    }
}
