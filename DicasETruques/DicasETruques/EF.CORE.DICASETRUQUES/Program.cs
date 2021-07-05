using EF.CORE.DICASETRUQUES.Data;
using EF.CORE.DICASETRUQUES.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EF.CORE.DICASETRUQUES
{
    class Program
    {
        static void Main(string[] args)
        {
            //ToQueryString();
            DebugView();
        }
        static void DebugView()
        {
            using var db = new ApplicationContext();

            //db.Database.EnsureCreated();

            
            db.Departamentos.Add( new Departamento {
            Descricao = "Teste - DebugView "
           
            });

            var query = db.Departamentos.Where(p => p.Id > 2);

            
            Console.ReadKey();
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
