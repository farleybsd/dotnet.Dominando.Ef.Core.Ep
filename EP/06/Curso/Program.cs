using System;
using System.Collections.Generic;
using System.Linq;
using Curso.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;

namespace DominandoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //FuncoesDeDatas();
            //FuncaoLike();
            //FuncaoDataLength();
            //FuncaoProperty();
            FuncaoCollate();
        }       

        static void FuncaoCollate()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {

                var consulta1 = db
                    .Funcoes
                    .FirstOrDefault(p=> EF.Functions.Collate(p.Descricao1, "SQL_Latin1_General_CP1_CS_AS") == "Tela");

                var consulta2 = db
                    .Funcoes
                    .FirstOrDefault(p=> EF.Functions.Collate(p.Descricao1, "SQL_Latin1_General_CP1_CI_AS") == "tela");

                Console.WriteLine($"Consulta1: {consulta1?.Descricao1}");
                
                Console.WriteLine($"Consulta2: {consulta2?.Descricao1}");
            }
        }

        static void FuncaoProperty()
        {
            ApagarCriarBancoDeDados();

            using (var db = new Curso.Data.ApplicationContext())
            {
                var resultado = db
                    .Funcoes
                    //.AsNoTracking()
                    .FirstOrDefault(p=> EF.Property<string>(p, "PropriedadeSombra") == "Teste");

                var propriedadeSombra = db
                    .Entry(resultado)
                    .Property<string>("PropriedadeSombra")
                    .CurrentValue;

                Console.WriteLine("Resultado:");     
                Console.WriteLine(propriedadeSombra); 
            }
        } 

        static void FuncaoDataLength()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                var resultado = db
                    .Funcoes
                    .AsNoTracking()
                    .Select(p => new 
                    {
                        TotalBytesCampoData = EF.Functions.DataLength(p.Data1),
                        TotalBytes1 = EF.Functions.DataLength(p.Descricao1),
                        TotalBytes2 = EF.Functions.DataLength(p.Descricao2),
                        Total1 = p.Descricao1.Length,
                        Total2 = p.Descricao2.Length
                    })
                    .FirstOrDefault();

                Console.WriteLine("Resultado:"); 

                Console.WriteLine(resultado); 
            }
        }

        static void FuncaoLike()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                var script = db.Database.GenerateCreateScript();

                Console.WriteLine(script);

                var dados = db
                    .Funcoes
                    .AsNoTracking()
                    //.Where(p=> EF.Functions.Like(p.Descricao1, "Bo%"))
                    .Where(p=> EF.Functions.Like(p.Descricao1, "B[ao]%"))
                    .Select(p => p.Descricao1)
                    .ToArray();

                Console.WriteLine("Resultado:");
                foreach (var descricao in dados)
                {
                    Console.WriteLine(descricao);
                }
            }
        }

        static void FuncoesDeDatas()
        {
            ApagarCriarBancoDeDados();

            using (var db = new Curso.Data.ApplicationContext())
            {
                var script = db.Database.GenerateCreateScript();

                Console.WriteLine(script);

                var dados = db.Funcoes.AsNoTracking().Select(p =>
                   new
                   {
                       Dias = EF.Functions.DateDiffDay(DateTime.Now, p.Data1),
                       Meses = EF.Functions.DateDiffMonth(DateTime.Now, p.Data1),
                       Data = EF.Functions.DateFromParts(2021, 1, 2),
                       DataValida = EF.Functions.IsDate(p.Data2),
                   });

                foreach (var f in dados)
                {
                    Console.WriteLine(f);
                }

            }
        }

        static void ApagarCriarBancoDeDados()
        {
            using var db = new Curso.Data.ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Funcoes.AddRange(
            new Funcao
            {
                Data1 = DateTime.Now.AddDays(2),
                Data2 = "2021-01-01",
                Descricao1 = "Bala 1 ",
                Descricao2 = "Bala 1 "
            },
            new Funcao
            {
                Data1 = DateTime.Now.AddDays(1),
                Data2 = "XX21-01-01",
                Descricao1 = "Bola 2",
                Descricao2 = "Bola 2"
            },
            new Funcao
            {
                Data1 = DateTime.Now.AddDays(1),
                Data2 = "XX21-01-01",
                Descricao1 = "Tela",
                Descricao2 = "Tela"
            });

            db.SaveChanges();
        }
    }
}
