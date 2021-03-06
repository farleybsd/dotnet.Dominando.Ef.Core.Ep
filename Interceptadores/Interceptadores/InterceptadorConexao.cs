using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Interceptadores.Interceptadores
{
    public class InterceptadorConexao : DbConnectionInterceptor
    {
        public override InterceptionResult ConnectionOpening(
           DbConnection connection,
           ConnectionEventData eventData,
           InterceptionResult result)
        {
            System.Console.WriteLine("Entrei no metodo ConnectionOpening");

            var connectionString = ((SqlConnection)connection).ConnectionString;

            System.Console.WriteLine(connectionString);

            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
            {
                //DataSource="IP Segundo Servidor",
                ApplicationName = "CursoEFCore"
            };

            connection.ConnectionString = connectionStringBuilder.ToString();

            System.Console.WriteLine(connectionStringBuilder.ToString());

            return result;
        }
    }
}

