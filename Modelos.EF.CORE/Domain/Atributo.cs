using Microsoft.EntityFrameworkCore;
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
    [Index(nameof(Descricao),nameof(Id),IsUnique=true)] //indice para o banco de dados
    [Comment("Meu comentario de minha tabela")] // comentario no banco de dados
    public class Atributo
    {
        [Key] // chave primaria
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]  Deixar o auto icremento com o banco de dados
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]  impende que o banco de dados gere o modelo como por exepmlo identy banco n ira colocar vc tem q coloca
        public int Id { get; set; }
        [Column("MinhaDescricao", TypeName = "VARCHAR(100)")] // nome da coluna e o tipo no banco
        [Comment("Meu comentario para meu campo")]
        public string Descricao { get; set; }
        ///[Required] // not null
        [MaxLength(255)] // tamanho maximo da coluna
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] //bloqueia o campo para insert update mas faz select
        public string Observacao { get; set; }
    }
    //Atributo Inverse Property
    public class Aeroporto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [NotMapped] // Quando nao quiser criar objeto ou uma propiedade no banco
        public string PropriedadeTeste { get; set; }
        [InverseProperty("AeroportoPartida")] // forcar o relacionamento
        public ICollection<Voo> VoosDePartida { get; set; }
        [InverseProperty("AeroportoChegada")] // forcar o relacionamento
        public ICollection<Voo> VoosDeChegada { get; set; }
    }

    [NotMapped] // Quando nao quiser criar objeto ou uma propiedade no banco
    public class Voo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Aeroporto AeroportoPartida { get; set; }
        public Aeroporto AeroportoChegada { get; set; }
    }
}
