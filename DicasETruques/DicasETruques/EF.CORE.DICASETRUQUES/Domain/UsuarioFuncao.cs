using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.CORE.DICASETRUQUES.Domain
{
    [Keyless]
    public class UsuarioFuncao
    {
        public Guid UsuarioId { get; set; }
        public Guid FuncaoId { get; set; }
    }
}
