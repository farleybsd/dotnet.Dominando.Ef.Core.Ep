using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Modelos.EF.CORE.Configurations;
using Modelos.EF.CORE.Conversores;
using Modelos.EF.CORE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.EF.CORE.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Conversor> Conversores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string strConnection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DevIo-02;Integrated Security=True;pooling=true";

            optionsBuilder
                .UseSqlServer(strConnection)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            // Adicionano colete no banco de dados de forma global
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI"); // CASE INSENSITIVE
            // RAFAEL -> rafael
            // Jõao -> Joao

            // propieade especifica
            modelBuilder.Entity<Departamento>().Property(p => p.Descricao).UseCollation("SQL_Latin1_General_CP1_CS_AS"); // CASE SENSITIVE E DIFERENCIAR ACENTOS
            */

            /*
            Cria um objeto de sequência e especifica suas propriedades.Uma sequência
            é um objeto associado a um esquema definido pelo usuário que gera uma 
            sequência de valores numéricos de acordo com a especificação com a 
            qual a sequência foi criada.A sequência de valores numéricos é gerada em 
            ordem crescente ou decrescente em um intervalo definido e pode ser configurada 
            para reiniciar(em um ciclo) quando se esgotar.As sequências, 
            ao contrário de colunas de identidade, não são associadas a tabelas específicas. 
            Os aplicativos fazem referência a um objeto de sequência para recuperar seu próximo valor. 
            A relação entre sequências e tabelas é controlada pelo aplicativo. 
            Os aplicativos de usuário podem referenciar um objeto de sequência e coordenar os valores 
            nas várias linhas e tabelas. */

            /*
            // Configurado uma sequence do Sql Server
            modelBuilder.HasSequence<int>("MinhaSequencia","sequencia")
                .StartsAt(1)       // valor inicial
                .IncrementsBy(2)  // quantidade que vai ser adicionado
                .HasMin(1)       // Valor minimo
                .HasMax(10)     // valor maxio
                .IsCyclic();   // reset a sequence para o valor minino

            //Usando a sequence criada
            modelBuilder.Entity<Departamento>().Property(p => p.Id).HasDefaultValueSql("NEXT VALUE FOR sequencia.MinhaSequencia");
            */

            /*
            // Indices
            modelBuilder
                .Entity<Departamento>()
                .HasIndex(p => new { p.Descricao, p.Ativo })
                .HasDatabaseName("idx_meu_indice_composto") // nome do indice na base de dados
                .HasFilter("Descricao IS NOT NULL") // filtrar o indice para deixar mais rapido a consulta
                .HasFillFactor(80) // VALOR FATOR PREENCHIMENTO DAS PAGINAS DE DADOS E O RESTO PARA  FAZER 100% SQL SERVER GERENCIA
                .IsUnique();// nao duplicar o indice; // indice composto de duas colunas
            */

            //   Propagação de dados - Criar a tabela ja com dados 
            //modelBuilder.Entity<Estado>().HasData(new[]
            //{
            //    new Estado{Id=1,Nome="Sao Paulo"},
            //    new Estado{Id=2,Nome="Sergipe"}
            //}); 
            // Modelo de semente Iniciais
            // na migracao o ef para o db as operacoes que podem ser realizada inset delete update

            // esquema de formal Global
            //modelBuilder.HasDefaultSchema("cadastros");
            //modelBuilder.Entity<Estado>().ToTable("Estados","SegundoEsquema");

            //Conversores de Valores no DB
            // consultar conversores EF
            //Microsoft.EntityFrameworkCore.Storage.ValueConversion.
            //var conversao = new ValueConverter<Versao, string>(p => p.ToString(), p => (Versao)Enum.Parse(typeof(Versao), p));

            //var conversao1 = new EnumToStringConverter<Versao>();

            //modelBuilder.Entity<Conversor>()
            //    .Property(p => p.Versao)
            //    .HasConversion(conversao);
            //.HasConversion(p => p.ToString(), p => (Versao)Enum.Parse(typeof(Versao), p)); // vai salvar como string na aplicacao vai ser um enum
            //.HasConversion<string>();

            // Conversor Customizado
            //modelBuilder.Entity<Conversor>()
            //            .Property(p => p.Status)
            //            .HasConversion(new ConversorCustomizado());

            //shadow Propeties
            //modelBuilder.Entity<Departamento>().Property<DateTime>("UltimaAtualizacao");

            //Owned Types Tipos Complexos
            //modelBuilder.Entity<Cliente>(p => {

            //    p.OwnsOne(x => x.Endereco,end=> 
            //    {
            //        end.Property(p => p.Bairro).HasColumnName("Bairro"); // Criando o nome na tabela do db
            //        end.ToTable("Endereco");
            //    });

            //});

            //Fluent Api
            // modelBuilder.ApplyConfiguration(new ClienteConfiguration()); modelo 1
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());// modo 2
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly); // modo3
        }
    }
}
