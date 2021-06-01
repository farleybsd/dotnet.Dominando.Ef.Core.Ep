using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modelos.EF.CORE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.EF.CORE.Conversores
{
    public class ConversorCustomizado : ValueConverter<Status,string>
    {
        public ConversorCustomizado() :base(p=>ConverterParaBancoDeDados(p),
                                            value => ConverterParaAplicacao(value),
                                            new ConverterMappingHints(1))
        {

        }

        static string ConverterParaBancoDeDados(Status status)
        {
            return status.ToString()[0..1];
        }
        static Status ConverterParaAplicacao(string value)
        {
            var status = Enum.GetValues<Status>().FirstOrDefault(p => p.ToString()[0..1]==value);
            return status;
        }
    }
}
