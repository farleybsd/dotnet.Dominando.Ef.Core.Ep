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
            //ConsultaParametrizada();
            //ConsultaInterpolada();
            //ConsultaComTag();
            //EntendendoConsulta1N1N1();
            //DivisaoDeConsultas();
            //CriarStoredProcedure();
            //InserirDadosViaProcedure();
            //CriarStoredProcedureDeConsulta();
            ConsultaViaProcedure();
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
                .Select(p =>
                new
                {
                    p.Descricao,
                    Funcionarios = p.Funcionarios.Select(f => new { f.Nome })
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
            var id = new SqlParameter
            {
                Value = 1,
                SqlDbType = System.Data.SqlDbType.Int
            };
            var departamentos = db.Departamentos
                .FromSqlRaw("Select * from Departamentos with(NOLOCK) where id >{0}", id)
                .Where(p => !p.Excluido)
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descricao:{departamento.Descricao}");

            }
        }
        static void ConsultaInterpolada()
        {
            using var db = new ApplicationContext();
            Setup(db);
            var id = 1;

            var departamentos = db.Departamentos
                .FromSqlInterpolated($"Select * from Departamentos with(NOLOCK) where id >{id}") // faz a transformacao para um DB parameeter
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descricao:{departamento.Descricao}");

            }
        }
        static void ConsultaComTag()
        {
            using var db = new ApplicationContext();
            Setup(db);


            var departamentos = db.Departamentos
                .TagWith(@"Estou enviando um comentario para o servidor
                           Segundo Comentario
                            Terceiro Comentario")
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descricao:{departamento.Descricao}");

            }
        }
        static void EntendendoConsulta1N1N1()
        {
            using var db = new ApplicationContext();
            Setup(db);

            var funcionarios = db.Funcionarios
              .Include(p => p.Departamento)
              .ToList();


            foreach (var funcionario in funcionarios)
            {
                Console.WriteLine($"\tNome:{funcionario.Nome} / Descricao Dep: {funcionario.Departamento.Descricao}");

            }

            /*
            var departamentos = db.Departamentos
                .Include(p => p.Funcionarios)
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descricao:{departamento.Descricao}");

                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\tNome:{funcionario.Nome}");

                }
            }
            */
        }
        static void DivisaoDeConsultas()
        {
            // Dados Duplicados
            using var db = new ApplicationContext();
            Setup(db);

            var departamentos = db.Departamentos
                .Include(p => p.Funcionarios)
                .Where(p => p.Id < 3)
                //.AsSplitQuery()
                .AsSingleQuery()// ignorar o AsSplitQuery
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine($"Descricao: {departamento.Descricao}");
                foreach (var funcionario in departamento.Funcionarios)
                {
                    Console.WriteLine($"\tNome: {funcionario.Nome}");
                }
            }
        }
        static void CriarStoredProcedure()
        {
            var criarDepartamento = @"

            CREATE OR ALTER PROCEDURE CriarDepartamento
                @Descricao VARCHAR(50),
                @Ativo bit
            as
               BEGIN
                INSERT INTO
                    Departamentos(Descricao,Ativo,Excluido)
                VALUES(@Descricao,@Ativo,0)
            END

            ";

            using var db = new ApplicationContext();
            db.Database.ExecuteSqlRaw(criarDepartamento);
            Console.WriteLine("Criado com Sucesso");
        }
        static void InserirDadosViaProcedure()
        {
            using var db = new ApplicationContext();

            db.Database.ExecuteSqlRaw("execute CriarDepartamento @p0,@p1","Departamento Via Procedure",true);
            Console.WriteLine("Criado com Sucesso");
        }
        static void CriarStoredProcedureDeConsulta()
        {
            var criarDepartamento = @"

            CREATE OR ALTER PROCEDURE GetDepartamentos
                @Descricao VARCHAR(50)
            as
               BEGIN
                SELECT * FROM Departamentos where Descricao like @Descricao + '%'
            END

            ";

            using var db = new ApplicationContext();
            db.Database.ExecuteSqlRaw(criarDepartamento);
            Console.WriteLine("Criado com Sucesso");
        }
        static void ConsultaViaProcedure()
        {
            using var db = new ApplicationContext();

            var dep = new SqlParameter("@dep", "Departamento");

            var departamentos = db.Departamentos
                //.FromSqlRaw("EXECUTE GetDepartamentos @p0", "Departamento")
                //.FromSqlRaw("EXECUTE GetDepartamentos @dep", dep)
                .FromSqlInterpolated($"EXECUTE GetDepartamentos {dep}")
                .ToList();

            foreach (var departamento in departamentos)
            {
                Console.WriteLine(departamento.Descricao);
            }

            Console.WriteLine("Criado com Sucesso");
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
