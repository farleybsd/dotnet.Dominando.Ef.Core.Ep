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
using System.Collections.Generic;

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
            //RelacionamentosUmParaMuitos();
            //RelacionamentoMuitosParaMuitos();
            //CamposDeApoio();
            //ExemploTph();
            //PacotesDePropriedades();
            //Atributos();
            DatabaseGeneratedComputed();
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
        static void RelacionamentoMuitosParaMuitos()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var ator1 = new Ator { Nome="Farley" };
                var ator2 = new Ator { Nome="Pires" };
                var ator3 = new Ator { Nome="Rafael" };

                var filme1 = new Filme { Descricao="A volta dos que não foram" };
                var filme2 = new Filme { Descricao = " De Volta para o futuro" };
                var filme3 = new Filme { Descricao = "Poeria em alto mar filme" };


                ator1.Filmes.Add(filme1);
                ator1.Filmes.Add(filme2);

                ator2.Filmes.Add(filme1);

                filme3.Atores.Add(ator1);
                filme3.Atores.Add(ator2);
                filme3.Atores.Add(ator3);

                db.AddRange(ator1, ator2, filme3);

                db.SaveChanges();

                foreach (var ator in db.Atores.Include(p => p.Filmes))
                {
                    Console.WriteLine($"Ator: {ator.Nome}");

                    foreach (var filme in ator.Filmes)
                    {
                        Console.WriteLine($"\tFilme: {filme.Descricao}");
                    }
                }
            }
        }
        static void CamposDeApoio() //Campo de apoio BACKING FIELD
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var documento = new Documento();
                documento.SetCpf("12345678933");

                db.Documentos.Add(documento);

                db.SaveChanges();

                foreach (var doc in db.Documentos.AsNoTracking())
                {
                    Console.WriteLine($"CPF => {doc.GetCPF()}");
                }
            }
        }
        static void ExemploTph()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var pessoa = new Pessoa {Nome = "Fulano de Tal" };
                var instrutor = new Instrutor { Nome = "Farley Rufino",Tecnologia=".NET",Desde= DateTime.Now };
                var aluno = new Aluno { Nome = "Rafael Ameilda",Idade=31,DataContrato=DateTime.Now };

                db.AddRange(pessoa,instrutor,aluno);
                db.SaveChanges();

                var pessoas = db.Pessoas.AsNoTracking().ToArray();
                var instrutores = db.Instrutors.AsNoTracking().ToArray();
                //var alunos = db.Alunos.AsNoTracking().ToArray();
                var alunos = db.Pessoas.OfType<Aluno>().AsNoTracking().ToArray();
                Console.WriteLine("**** Pesssoas ****");

                foreach (var p in pessoas)
                {
                    Console.WriteLine($"Id: {p.Id} -> {p.Nome}");
                }

                Console.WriteLine("**** Instutores ****");

                foreach (var p in instrutores)
                {
                    Console.WriteLine($"Id: {p.Id} -> {p.Nome}, Tecnologia: {p.Tecnologia}, Desde: {p.Desde}");
                }

                Console.WriteLine("**** Alunos ****");

                foreach (var p in alunos)
                {
                    Console.WriteLine($"Id: {p.Id} -> {p.Nome}, Idade: {p.Idade}, Data Contrato: {p.DataContrato}");
                }
            }
        }
        static void PacotesDePropriedades()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var configuracao = new Dictionary<string, object>
                {
                    ["Chave"] = "SenhaBancoDeDados",
                    ["Valor"] = Guid.NewGuid().ToString()
                };

                db.configuracoes.Add(configuracao);
                db.SaveChanges();

                var configuracoes = db
                    .configuracoes
                    .AsNoTracking()
                    .Where(p => p["Chave"] == "SenhaBancoDeDados")
                    .ToArray();

                foreach (var dic in configuracoes)
                {
                    Console.WriteLine($"Chave: {dic["Chave"]} - Valor: {dic["Valor"]}");
                }
            }
        }
        static void Atributos() // DataNotations
        {
            using (var db = new ApplicationContext())
            {
                var script = db.Database.GenerateCreateScript(); // script de geracao do banco
                Console.WriteLine(script);
            }
        }
        static void DatabaseGeneratedComputed()
        {
            using (var db = new ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var script = db.Database.GenerateCreateScript();

                Console.WriteLine(script);

                db.Atributos.Add(new Atributo {Descricao="Exemplo",Observacao="observacao" });

                db.SaveChanges();
            }
        }
    }
}
