using Microsoft.EntityFrameworkCore;
using Performace.Data;
using Performace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Performace
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup();
            //ConsultaRastreada();
            //ConsultaNaoRastreada();
            //ConsultaComResolucaoIdentidade();
            //ConsultaProjetadaERastreada();
            //Inserir_200_departamento_Com_1MB();
            ConsultaProjetada();
            Console.ReadKey();
        }
        static void Setup()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Departamentos.Add(new Departamento
            {

                Descricao = "Departamento Teste",
                Ativo = true,
                Funcionarios = Enumerable.Range(1, 100).Select(p => new Funcionario // vai criar 100 funcionario automaticamente
                {
                    Cpf = p.ToString().PadLeft(11, '0'),
                    Nome = $"Funcionario {p}",
                    Rg = p.ToString()
                }).ToList()

            });

            db.SaveChanges();
        }
        static void ConsultaRastreada()
        {
            //vai no banco faz uma copia dos resultados cria um snapshot
            // e usa o contexto como cache sem fazer new para cada include
            using var db = new ApplicationContext();

            var funcionario = db.Funcionarios.Include(p => p.Departamento).ToList();
        }

        static void ConsultaNaoRastreada()
        {
            // Consulta somente leitura
            // Ele faz um select no banco e da um  new para cada include
            using var db = new ApplicationContext();

            var funcionario = db.Funcionarios.AsNoTracking().Include(p => p.Departamento).ToList();
        }

        static void ConsultaComResolucaoIdentidade()
        {
            // Consulta somente leitura
            // Ele faz um select no banco sem da um  new para cada include
            // sem rastrear
            using var db = new ApplicationContext();

            var funcionario = db.Funcionarios
                .AsNoTrackingWithIdentityResolution()
                .Include(p => p.Departamento)
                .ToList();
        }
        static void ConsultaCustomizada()
        {

            using var db = new ApplicationContext();

            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            var funcionario = db.Funcionarios
                //.AsNoTrackingWithIdentityResolution()
                .Include(p => p.Departamento)
                .ToList();
        }
        static void ConsultaProjetadaERastreada()
        {
            // so de retornar a entidade ela ja fica rastreada
            using var db = new ApplicationContext();

            //db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            var departamentos = db.Departamentos
                //.AsNoTrackingWithIdentityResolution()
                .Include(p => p.Funcionarios)
                .Select(p => new
                {
                    Departamento = p,
                    TotolFuncionario = p.Funcionarios.Count()
                })
                .ToList();

            departamentos[0].Departamento.Descricao = "Departamento Teste Atualizado";
            db.SaveChanges();
        }
        static void Inserir_200_departamento_Com_1MB()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Departamentos.AddRange(Enumerable.Range(1, 200).Select(p => new Departamento
            {

                Descricao = "Departamento Teste",
                Image = getBytes()
            }));

            db.SaveChanges();

            byte[] getBytes()
            {
                var buffer = new byte[1024 * 1024];
                Random rnd = new Random();
                rnd.NextBytes(buffer);
                return buffer;
            }
        }
        static void ConsultaProjetada()
        {
            
            using var db = new ApplicationContext();

            //var departamentos = db.Departamentos.ToArray();
            var departamentos = db.Departamentos.Select(p=>p.Descricao).ToArray();

            var memoria = (System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024) + "MB";

            Console.WriteLine(memoria);
        }
    }
}
