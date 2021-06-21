using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionsDb.FuncoesDB
{
    public static class MinhasFunçoes
    {
        public static string LetrasMaiusculas(string dados)
        {
            throw new NotImplementedException();
        }
        public static void RegistrarFuncoes(ModelBuilder modelBuilder)
        {
            var funcoes = typeof(MinhasFunçoes).GetMethods().Where(p => Attribute.IsDefined(p, typeof(DbFunctionAttribute)));

            foreach (var funcao in funcoes)
            {
                modelBuilder.HasDbFunction(funcao);
            }
        }

        [DbFunction(name: "LEFT", schema: "", IsBuiltIn = true)] // funcao incorporada do db funcao nativa
        public static string Left(string dados, int quantidade)
        {
            throw new NotImplementedException();
        }
    }
}
