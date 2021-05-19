using Dotnet.EfCore.Consultas.Data;
using Dotnet.EfCore.Consultas.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Dotnet.EfCore.Consultas
{
    class Program
    {
        static void Main(string[] args)
        {
            //FiltroGlobal();
            //IgnoreFiltroGlobal();
            //ConsultaProjetada();
            ConsultaParametrizada();
            Console.ReadKey();

        }

        static void FiltroGlobal()
        {
            using var db = new ApplicationContext();
            Setup(db);

            var departamentos = db.Departamentos.Where(p => p.Id > 0).ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descricao:{departamento.Descricao} \t Excluido: {departamento.Excluido}");
            }
        }

        static void IgnoreFiltroGlobal()
        {
            using var db = new ApplicationContext();
            Setup(db);

            var departamentos = db.Departamentos.IgnoreQueryFilters().Where(p => p.Id > 0).ToList();
                   
            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descricao:{departamento.Descricao} \t Excluido: {departamento.Excluido}");
            }
        }
        static void ConsultaProjetada()
        {
            using var db = new ApplicationContext();
            Setup(db);

            var departamentos = db.Departamentos
                .Where(p => p.Id > 0)
                .Select(p=> 
                new 
                { 
                    p.Descricao,
                    Funcionarios = p.Funcionarios.Select(f => new {f.Nome })
                })
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descricao:{departamento.Descricao}");
                
                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\t Nome:{funcionario.Nome}");
                }
            }
        }
        static void ConsultaParametrizada()
        {
            using var db = new ApplicationContext();
            Setup(db);
            var id =  new SqlParameter
            { 
                Value =1,
                SqlDbType = System.Data.SqlDbType.Int
            };
            var departamentos = db.Departamentos
                .FromSqlRaw("Select * from Departamentos with(NOLOCK) where id >{0}",id)
                .Where(p=> !p.Excluido)
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descricao:{departamento.Descricao}");
                
            }
        }
        static void Setup(ApplicationContext db)
        {
            if (db.Database.EnsureCreated())
            {
                db.Departamentos.AddRange(
                    new Departamento
                    {
                        Ativo = true,
                        Descricao = "Departamento 01",
                        Funcionarios = new System.Collections.Generic.List<Funcionario>
                        {
                            new Funcionario
                            {
                                Nome = "Rafael Almeida",
                                Cpf = "99999999911",
                                Rg= "2100062"
                            }
                        },
                        Excluido = true
                    },
                    new Departamento
                    {
                        Ativo = true,
                        Descricao = "Departamento 02",
                        Funcionarios = new System.Collections.Generic.List<Funcionario>
                        {
                            new Funcionario
                            {
                                Nome = "Bruno Brito",
                                Cpf = "88888888811",
                                Rg= "3100062"
                            },
                            new Funcionario
                            {
                                Nome = "Eduardo Pires",
                                Cpf = "77777777711",
                                Rg= "1100062"
                            }
                        }
                    });

                db.SaveChanges();
                db.ChangeTracker.Clear();
            }
        }
    }
}
