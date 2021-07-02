using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.UoW.Repository.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        //Relacionamento 1xN
        public List<Colaborador> Colaboradores { get; set; } //N
    }
}
