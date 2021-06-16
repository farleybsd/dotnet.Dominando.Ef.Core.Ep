using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Interceptadores.Interceptadores
{
    public class InterceptadoresDeCommandos : DbCommandInterceptor
    {
        private static readonly Regex _tableRegex =
           new Regex(@"(?<tableAlias>FROM +(\[.*\]\.)?(\[.*\]) AS (\[.*\])(?! WITH \(NOLOCK\)))",
               RegexOptions.Multiline |
               RegexOptions.IgnoreCase |
               RegexOptions.Compiled);
        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command, 
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            System.Console.WriteLine("[Sync] Entrei no metodo ReaderExecuting ");
            UsarNoLock(command);
            return result;
        }

        public override ValueTask<DbDataReader> ReaderExecutedAsync(
            DbCommand command,
            CommandExecutedEventData eventData,
            DbDataReader result,
            CancellationToken cancellationToken = default)
        {
            System.Console.WriteLine("[Async] Entrei no metodo ReaderExecuting ");
            UsarNoLock(command);
            return new ValueTask<DbDataReader>(result);
        }

        private static void UsarNoLock(DbCommand command)
        {
            //if (!command.CommandText.Contains("WITH (NOLOCK)"))
            if (!command.CommandText.Contains("WITH (NOLOCK)")
              && command.CommandText.StartsWith("-- Use NOLOCK"))
            {
                command.CommandText = _tableRegex.Replace(command.CommandText, "${tableAlias} WITH (NOLOCK)");
            }
        }
    }
}
