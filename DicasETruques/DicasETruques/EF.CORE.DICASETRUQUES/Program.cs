using EF.CORE.DICASETRUQUES.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EF.CORE.DICASETRUQUES
{
    class Program
    {
        static void Main(string[] args)
        {
            ToQueryString();
        }

        static void ToQueryString()
        {
            using var db = new ApplicationContext();
            
            db.Database.EnsureCreated();

            var query = db.Departamentos.Where(p=> p.Id > 2);

            var sql = query.ToQueryString();

            Console.WriteLine(sql);
            Console.ReadKey();
        }
    }
}
