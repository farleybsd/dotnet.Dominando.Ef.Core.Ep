using Comportamentos_EF_Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Comportamentos_EF_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            var sql = db.Departamentos.Where(p => p.Id > 0).ToQueryString();

            Console.WriteLine(sql);
            Console.ReadKey();
        }
    }
}
