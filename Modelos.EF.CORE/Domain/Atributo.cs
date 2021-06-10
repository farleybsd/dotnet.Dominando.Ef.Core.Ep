using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.EF.CORE.Domain
{
    [Table("TabelaAtributos")]
    public class Atributo
    {
        [Key] // chave primaria
        public int Id { get; set; }
        [Column("MinhaDescricao", TypeName = "VARCHAR(100)")] // nome da coluna e o tipo no banco
        public string Descricao { get; set; }
        [Required] // not null
        [MaxLength(255)] // tamanho maximo da coluna
        public string Observacao { get; set; }
    }
    //Atributo Inverse Property
    public class Aeroporto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [InverseProperty("AeroportoPartida")] // forcar o relacionamento
        public ICollection<Voo> VoosDePartida { get; set; }
        [InverseProperty("AeroportoChegada")] // forcar o relacionamento
        public ICollection<Voo> VoosDeChegada { get; set; }
    }

    public class Voo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Aeroporto AeroportoPartida { get; set; }
        public Aeroporto AeroportoChegada { get; set; }
    }
}
