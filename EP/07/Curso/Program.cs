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
            //TesteInterceptacao();
            TesteInterceptacaoSaveChanges();
        }       

        static void TesteInterceptacaoSaveChanges()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Funcoes.Add(new Funcao
                {
                    Descricao1 = "Teste"
                });

                db.SaveChanges();
            }
        }

        static void TesteInterceptacao()
        {
            using (var db = new Curso.Data.ApplicationContext())
            {
                var consulta = db
                    .Funcoes
                    .TagWith("Use NOLOCK")
                    .FirstOrDefault(); 

                Console.WriteLine($"Consulta: {consulta?.Descricao1}");
            }
        }
    }
}
