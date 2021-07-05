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
            //DebugView();
            //ClearContext();
            //ConsultaFiltrada();
            //SingleOrDefaultVsFirstOrDefault();
            //SemChavePrimaria();
            ToView();
        }
        static void ToView()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Database.ExecuteSqlRaw(
               @"CREATE VIEW vw_departamento_relatorio AS
                SELECT
                    d.Descricao, count(c.Id) as Colaboradores
                FROM Departamentos d 
                LEFT JOIN Colaboradores c ON c.DepartamentoId=d.Id
                GROUP BY d.Descricao");

            var departamentos = Enumerable.Range(1, 10)
                .Select(p => new Departamento
                {
                    Descricao = $"Departamento {p}",
                    colaboradores=Enumerable.Range(1,p)
                    .Select(c=> new Colaborador
                    {
                        Nome = $"Colaborador {p}-{c}"
                    }).ToList()
                });

            var departamento = new Departamento
            {
                Descricao = $"Departamento Sem Colaborador"
            };

            db.Departamentos.Add(departamento);
            db.Departamentos.AddRange(departamentos);
            db.SaveChanges();

            var relatorio = db.DepartamentoRelatorio
                .Where(p => p.Colaboradores < 20)
                .OrderBy(p => p.Departamento)
                .ToList();

            foreach (var dep in relatorio)
            {
                Console.WriteLine($"{dep.Departamento} [ Colaboradores: {dep.Colaboradores}]");
            }
        }
        static void SemChavePrimaria()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var UsuarioFuncao = db.UsuarioFuncoes
                                  .Where(p => p.UsuarioId == Guid.NewGuid())
                                  .ToArray();
        }
        static void SingleOrDefaultVsFirstOrDefault()
        {
            using var db = new ApplicationContext();

            Console.WriteLine("SingleOrDefault:");

            _ = db.Departamentos.SingleOrDefault(p => p.Id > 2);

            Console.WriteLine("FirstOrDefault:");

            _ = db.Departamentos.FirstOrDefault(p => p.Id > 2);
        }
        static void ConsultaFiltrada()
        {
            using var db = new ApplicationContext();

            var sql = db.Departamentos.Include(p =>
                p.colaboradores
                .Where(c => c.Descricao.Contains("Teste"))
                   ).ToQueryString();

            Console.WriteLine(sql);
            Console.ReadKey();
        }
        static void ClearContext()
        {
            using var db = new ApplicationContext();




            db.Departamentos.Add(new Departamento
            {
                Descricao = "Teste - DebugView "

            });


            db.ChangeTracker.Clear(); // redefinir o estado do contexto

            Console.ReadKey();
        }
        static void DebugView()
        {
            using var db = new ApplicationContext();

            //db.Database.EnsureCreated();


            db.Departamentos.Add(new Departamento
            {
                Descricao = "Teste - DebugView "

            });

            var query = db.Departamentos.Where(p => p.Id > 2);


            Console.ReadKey();
        }
        static void ToQueryString()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureCreated();

            var query = db.Departamentos.Where(p => p.Id > 2);

            var sql = query.ToQueryString();

            Console.WriteLine(sql);
            Console.ReadKey();
        }
    }
}
