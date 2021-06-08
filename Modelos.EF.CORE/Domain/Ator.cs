using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.EF.CORE.Domain
{
    //Configurando Relacionamento muitos-para-muitos
    // Obrigado a ter referencia da propiedade em ambos os lados
    // Ator conhecer Filme
    //Filme conhecer Ator
    public class Ator
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Filme> Filmes { get; set; } = new List<Filme>();
    }

    public class Filme
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<Ator> Atores { get; set; } = new List<Ator>();
    }
}
