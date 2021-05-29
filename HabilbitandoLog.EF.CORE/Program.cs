using HabilbitandoLog.EF.CORE.Data;
using HabilbitandoLog.EF.CORE.Domain;
using System;
using System.Linq;

namespace HabilbitandoLog.EF.CORE
{
    class Program
    {
        static void Main(string[] args)
        {
            HabilitandoBatchSize();
            //DadosSensiveis();
            //ConsultarDepartamentos();
            Console.ReadKey();
        }

        static void ConsultarDepartamentos()
        {
            using var db = new ApplicationContext();

            var departamento = db.Departamentos.Where(p => p.Id > 0).ToArray();
        }
        static void DadosSensiveis()
        {
            using var db = new ApplicationContext();

            var descricao = "Departamento";

            var departamento = db.Departamentos.Where(p => p.Descricao == descricao).ToArray();
        }
        static void HabilitandoBatchSize()
        // quantidade de registro que o banco ira quebrar por vez para realizar um inert
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            for (int i = 0; i < 50; i++)
            {
                db.Departamentos.Add(
                    new Departamento
                    {
                        Descricao = "Departamento" + i
                    });
            }

            db.SaveChanges();
        }
    }
}
