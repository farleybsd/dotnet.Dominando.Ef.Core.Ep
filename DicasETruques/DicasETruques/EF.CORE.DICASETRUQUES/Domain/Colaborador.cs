using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.CORE.DICASETRUQUES.Domain
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
    }
}
