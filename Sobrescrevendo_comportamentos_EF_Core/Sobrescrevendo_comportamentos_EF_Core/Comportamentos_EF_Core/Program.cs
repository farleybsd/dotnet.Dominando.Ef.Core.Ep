using Comportamentos_EF_Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace Comportamentos_EF_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            DiagnosticListener.AllListeners.Subscribe(new MyInterceptorListener()); 
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            //var sql = db.Departamentos.Where(p => p.Id > 0).ToQueryString();
            _ = db.Departamentos.Where(p => p.Id > 0).ToArray();
            //Console.WriteLine(sql);
            Console.ReadKey();
        }
    }
}
