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
            //PropagarDados();
            //Esquema();
            ConversorDeValor();
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
        static void Esquema()
        {
            // Criar a tabela ja com dados 
            using var db = new ApplicationContext();

            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        static void ConversorDeValor() => Esquema();
    }
}
