using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interceptadores.Interceptadores
{
    public class InterceptadoresDeCommandos : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command, 
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            System.Console.WriteLine("[Sync] Entrei no metodo ReaderExecuting ");
            return base.ReaderExecuting(command, eventData, result);
        }

        public override ValueTask<DbDataReader> ReaderExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result,
            CancellationToken cancellationToken = default)
        {
            System.Console.WriteLine("[Async] Entrei no metodo ReaderExecuting ");
            return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }
    }
}
