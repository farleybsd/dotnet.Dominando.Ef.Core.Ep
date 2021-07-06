using EFCORE.TESTS.Data;
using EFCORE.TESTS.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace EFCORE.TESTS
{
    public class SqLiteTest
    {
        [Theory]
        [InlineData("Tecnologia")]
        [InlineData("Financeiro")]
        [InlineData("Departamento Pessoal")]
        public void Deve_Inserir_E_Consultar_Um_Departamento(string descricao)
        {
            // Arrange
            var derpartamento = new Departamento
            {
                Descricao = descricao,
                DataCadastro = DateTime.Now
            };

            // Setup
            var context = CreateContext();
            context.Database.EnsureCreated();
            context.Departamentos.Add(derpartamento);

            // Act
            var inseridos = context.SaveChanges();
            derpartamento = context
                            .Departamentos
                            .FirstOrDefault(p => p.Descricao == descricao);

            // Assert
            Assert.Equal(1, inseridos);
            Assert.Equal(descricao, derpartamento.Descricao);
        }

        private ApplicationContext CreateContext()
        {
            var conexao = new SqliteConnection("Datasource=:memory:");
            conexao.Open();

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite(conexao)
                .Options;

            return new ApplicationContext(options);
        }
    }
}
