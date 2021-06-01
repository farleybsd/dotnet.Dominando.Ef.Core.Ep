using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.EF.CORE.Domain
{
    public class Conversor
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }
        public Versao Versao { get; set; }
        public IPAddress EnderecoIp { get; set; }
    }

    public enum Versao
    {
        EFCore1,
        EFCore2,
        EFCore3,
        EFCore4,
        EFCore5,
    }
}
