using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transacoes.Domain
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        [Column(TypeName ="VARCHAR(15)")]
        public string Autor { get; set; }
    }
}
