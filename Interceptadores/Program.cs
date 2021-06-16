using Interceptadores.Data;
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
                                .FirstOrDefault();

                Console.WriteLine($"Consulta:{consulta?.Descricao1}");
            }
        }
    }
}
