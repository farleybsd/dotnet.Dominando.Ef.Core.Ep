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
using System.Text.Json;

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
            //TrabalhandoComPropiedadesDeSombra();
            //TiposDePropiedades();
            //RelacionamentoUmParaUm();
            RelacionamentosUmParaMuitos();
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
        static void TiposDePropiedades()
        {
            //Owned Types

            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var cliente = new Cliente
            {
                Nome = "Fulano de Tal",
                Telefone = "(79) 98888-9999",
                Endereco = new Endereco {Bairro="Centro",Cidade="Sao Paulo" }
                
            };

            db.Clientes.Add(cliente);
            db.SaveChanges();

            var clientes = db.Clientes.AsNoTracking().ToList();

            var options = new JsonSerializerOptions { WriteIndented = true };

            clientes.ForEach( cli => {

                var json = JsonSerializer.Serialize(cli, options);

                Console.WriteLine(json);
            });
        }
        static void RelacionamentoUmParaUm()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var estado = new Estado { Nome="Sergipe",Governador = new Governador { Nome="Rafael Almeida" } };
            
            db.Estados.Add(estado);

            db.SaveChanges();

            //var estados = db.Estados.Include(p=> p.Governador).AsNoTracking().ToList();
            var estados = db.Estados.AsNoTracking().ToList();
            estados.ForEach(estado => { Console.WriteLine($"Estado: {estado.Nome}, Governador: {estado.Governador.Nome}"); });
        }

        static void RelacionamentosUmParaMuitos()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var estado = new Estado
                {
                    Nome = "Sergipe",
                    Governador = new Governador { Nome = "Rufinão" }
                };

                estado.Cidades.Add(new Cidade { Nome = "Itabaiana" });

                db.Estados.Add(estado);

                db.SaveChanges();
            }

            using (var db = new ApplicationContext())
            {
                var estado = db.Estados.ToList();

                estado[0].Cidades.Add(new Cidade { Nome = "Aracaju" });

                db.SaveChanges();

                foreach (var est in db.Estados.Include(p => p.Cidades).AsNoTracking())
                {
                    Console.WriteLine($"Estado: {est.Nome}, Governador: {est.Governador.Nome}");
                    
                    foreach (var cidade in est.Cidades)
                    {
                        Console.WriteLine($"\t Cidade: {cidade.Nome}");
                    }
                }
            }
        }

    }
}
