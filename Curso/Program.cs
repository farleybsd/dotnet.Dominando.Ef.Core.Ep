using DominandoEfCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace DominandoEfCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //EnsureDeleted();
            //GapEnsureCreated();
            //HealthCheckBancoDados();
            //_count = 0;
            //GerenciarEstadoDaConexao(false);
            //_count = 0;
            //GerenciarEstadoDaConexao(true);

            Console.ReadKey();
        }
        // Se o banco nao exister ele cria
        static void EnsureCreatedAndDelete() 
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();
        }

        // Deleta todo o banco
        static void EnsureDeleted()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
        }

        //Criando tabela de um outro contexto em um outro banco
        static void GapEnsureCreated()
        {
            using var db1 = new ApplicationContext();
            using var db2 = new ApplicationContextCidade();

            db1.Database.EnsureCreated();
            db2.Database.EnsureCreated();

            var databaseCretador = db2.GetService<IRelationalDatabaseCreator>();
            databaseCretador.CreateTables();
        }
        //Checando o Banco de Dados se esta On
        static void HealthCheckBancoDados()
        {
            using var db = new ApplicationContext();
            var canConnect = db.Database.CanConnect();

            if (canConnect)
            {
                Console.WriteLine("Posso Me connectar");
            }
            else
            {
                Console.WriteLine("Nao Posso Me connectar");
            }
            //try
            //{
            //    // Poder Usar a forma 1 ou a dois

            //    //1 Forma mais usada
            //    var conection = db.Database.GetDbConnection();
            //    conection.Open();
            //    //2 Forma  menos usada
            //    db.Departamentos.Any();
            //    Console.WriteLine("Posso Me connectar");
            //}
            //catch (Exception)
            //{

            //    Console.WriteLine("Nao Posso Me connectar");
            //}
        }

        static int _count;
        static void GerenciarEstadoDaConexao(bool gerenciarEstadoConexao)
        {
            using var db = new ApplicationContext();
            var time = System.Diagnostics.Stopwatch.StartNew();

            var conexao = db.Database.GetDbConnection();

            conexao.StateChange += (_, ____) => ++_count;

            if (gerenciarEstadoConexao)
            {
                conexao.Open();
            }

            for (var i = 0; i < 200; i++)
            {
                db.Departamentos.AsNoTracking().Any();
            }

            time.Stop();
            var mensagem = $"Tempo:{time.Elapsed.ToString()},{gerenciarEstadoConexao}, Contador : {_count}";
            Console.WriteLine(mensagem);
        }

        static void ExeculteSql()
        {
            using var db = new ApplicationContext();

            //Primeira Opcao
            using (var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "Select 1";
                cmd.ExecuteNonQuery();
            }

            // Segunda Opcao evita sql injection
            var descricao = "TESTE";
            db.Database.ExecuteSqlRaw("update departamentos set descricao={0} where id=1",descricao);

            // Terceira Opcao
            db.Database.ExecuteSqlInterpolated($"update departamentos set descricao={0} where id=1");
        }
    }
}
