using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Comportamentos_EF_Core
{
    public class MyInterceptor : IObserver<KeyValuePair<string, object>>
    {
        private static readonly Regex _tableAliasRegex =
            new Regex(@"(?<tableAlias>FROM +(\[.*\]\.)?(\[.+\]) AS (\[.*\])(?! WITH \(NOLOCK\)))",
            RegexOptions.Multiline  |
            RegexOptions.IgnoreCase |
            RegexOptions.Compiled);

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            if (value.Key == RelationalEventId.CommandExecuting.Name)
            {
                var command = ((CommandEndEventData)value.Value).Command;
                
                if (!command.CommandText.Contains("WITH (NOLOCK)"))
                {
                    command.CommandText = _tableAliasRegex.Replace(command.CommandText, "${tableAlias} WITH (NOLOCK)");
                }
            }
            
        }
    }
}
