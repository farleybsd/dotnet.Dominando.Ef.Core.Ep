using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Meu.namespace
{
    public partial class Pessoa
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60)]
        public string Nome { get; set; }
    }
}
