using Modelos.EF.CORE.Data;
using System;

namespace Modelos.EF.CORE
{
    class Program
    {
        static void Main(string[] args)
        {
            Collations();
            Console.ReadKey();
        }

        static void Collations()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
