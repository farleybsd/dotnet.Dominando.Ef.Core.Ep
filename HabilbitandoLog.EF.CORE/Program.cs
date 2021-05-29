using HabilbitandoLog.EF.CORE.Data;
using System;
using System.Linq;

namespace HabilbitandoLog.EF.CORE
{
    class Program
    {
        static void Main(string[] args)
        {
            DadosSensiveis();
            //ConsultarDepartamentos();
            Console.ReadKey();
        }

        static void ConsultarDepartamentos()
        {
            using var db = new  ApplicationContext();

            var departamento = db.Departamentos.Where(p => p.Id > 0).ToArray();
        }
        static void DadosSensiveis()
        {
            using var db = new ApplicationContext();

            var descricao = "Departamento";

            var departamento = db.Departamentos.Where(p => p.Descricao == descricao).ToArray();
        }
    }
}
