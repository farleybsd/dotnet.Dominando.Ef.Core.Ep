using EFCORE.TESTS.Data;
using EFCORE.TESTS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace EFCORE.TESTS
{
    public class ImMemoryTest
    {
        [Fact]
        public void Deve_Inserir_Um_Departamento()
        {
            // Arrange
            var derpartamento = new Departamento
            {
                Descricao = "Tecnologia",
                DataCadastro = DateTime.Now
            };

            // Setup
            var context = CreateContext();
            context.Departamentos.Add(derpartamento);

            // Act
            var inseridos = context.SaveChanges();

            // Assert
            Assert.Equal(1, inseridos);
        }

        [Fact]
        public void Nao_Implementado_funcoes_de_datas_para_o_provider_inmemory()
        {
            // Arrange
            var derpartamento = new Departamento
            {
                Descricao = "Tecnologia",
                DataCadastro = DateTime.Now
            };

            // Setup
            var context = CreateContext();
            context.Departamentos.Add(derpartamento);

            // Act
            var inseridos = context.SaveChanges();

            // Assert
            Action action = () => context
                                  .Departamentos
                                  .FirstOrDefault(p => EF.Functions.DateDiffDay(DateTime.Now,p.DataCadastro) > 0 );

            Assert.Throws<InvalidOperationException>(action);
        }

        private ApplicationContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("InMemoryTest")
                .Options;

            return new ApplicationContext(options);
        }
    }
}
