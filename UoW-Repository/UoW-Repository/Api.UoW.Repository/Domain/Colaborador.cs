using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.UoW.Repository.Domain
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        //Relacionamento 1xN
        public int DepartamenoId { get; set; }
        public Departamento Departamento { get; set; } //1
    }
}
