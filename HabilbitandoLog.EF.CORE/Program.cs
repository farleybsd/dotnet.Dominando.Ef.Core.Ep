using HabilbitandoLog.EF.CORE.Data;
using System;
using System.Linq;

namespace HabilbitandoLog.EF.CORE
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsultarDepartamentos();
            Console.ReadKey();
        }

        static void ConsultarDepartamentos()
        {
            using var db = new  ApplicationContext();

            var departamento = db.Departamentos.Where(p => p.Id > 0).ToArray();
        }
    }
}
