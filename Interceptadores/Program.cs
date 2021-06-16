using Interceptadores.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Interceptadores
{
    class Program
    {
        static void Main(string[] args)
        {
            TesteInterceptacao();
            Console.ReadKey();
        }

        static void TesteInterceptacao()
        {
            using (var db = new ApplicationContext())
            {
                var consulta = db
                                .Funcoes
                                .TagWith("Use NOLOCK")
                                .FirstOrDefault();

                Console.WriteLine($"Consulta:{consulta?.Descricao1}");
            }
        }
    }
}
