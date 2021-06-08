using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.EF.CORE.Domain
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        // Propiedade de referencia no estado para Goovernador relacionameto 1x1
        public Governador Governador { get; set; }
        public ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();
    }

    public  class Governador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int Partido { get; set; }
        public int EstadoId { get; set; } // fk governador e depedente de um estado POR ISSO E OBG A TER UMA FK
        //Propiedade de referencia no governador  para o estado relacionameto 1x1
        public Estado Estado { get; set; }
    }

    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}
