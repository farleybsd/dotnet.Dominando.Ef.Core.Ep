using Microsoft.EntityFrameworkCore;
using Modelos.EF.CORE.Conversores;
using Modelos.EF.CORE.Data;
using Modelos.EF.CORE.Domain;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Data.SqlClient;
using Modelos.EF;
namespace Modelos.EF.CORE
{
    class Program
    {
        static void Main(string[] args)
        {
            //Collations();
            //PropagarDados();
            //Esquema();
            //ConversorDeValor();
            //ConversorCustomizado();
            //PropriedadesDeSombra();
            TrabalhandoComPropiedadesDeSombra();
            Console.ReadKey();
        }

        static void Collations()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        static void PropagarDados()
        {
            // Criar a tabela ja com dados 
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }
        static void Esquema()
        {
            // Criar a tabela ja com dados 
            using var db = new ApplicationContext();

            var script = db.Database.GenerateCreateScript();
            Console.WriteLine(script);
        }

        static void ConversorDeValor() => Esquema();
        static void ConversorCustomizado()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Conversores.Add( new Conversor 
            {
                Status = Status.Devolvido
            });

            db.SaveChanges();

            var conversorEmAnalise = db.Conversores.AsNoTracking().FirstOrDefault(p => p.Status == Status.Analise);
            var conversorDeVolvido = db.Conversores.AsNoTracking().FirstOrDefault(p => p.Status == Status.Devolvido);
        }
        static void PropriedadesDeSombra()
        {
            using var db = new ApplicationContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
        static void TrabalhandoComPropiedadesDeSombra()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();
            db.Database.EnsureCreated();

            var departamento = new Departamento
            {
                Descricao = "Departamento Propiedade de Sombra"
            };

            db.Departamentos.Add(departamento);
            //shadow Propeties
            db.Entry(departamento).Property("UltimaAtualizacao").CurrentValue = DateTime.Now;
            db.SaveChanges();
        }
        static void ConsultadoshadowPropeties()
        {
            using var db = new  ApplicationContext();

           // var departamentos = db.Departamentos.Where(p => EF.Property<DateTime>(p, "UltimaAtualizacao") < DateTime.Now).ToArray();
        }

    }
}
