using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelos.EF.CORE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.EF.CORE.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            //Owned Types Tipos Complexos
            //Relacionamento 1X1
            builder.HasOne(p=>p.Governador)
                .WithOne(p=> p.Estado)
                .HasForeignKey<Governador>(P=>P.EstadoId); // COLOCAR O RELACIONAMENTO DEPENDE

            builder.Navigation(P => P.Governador).AutoInclude(); // NAO PRECISAR O METODO INCLUDE PARA  TRAZER DADOS USANDO O JOIN

            //Configurando Relacionamento um-para-muitos

            builder.HasMany(p => p.Cidades)
                //.WithOne() // configurando propiedade de navegacao unica quando n tenho estado dentro da cidade
                .WithOne(p => p.Estado);
                //.IsRequired(false) removendo a depedencia da fk vc pode criar uma cidade sem ter estado
                //.OnDelete(DeleteBehavior.Restrict)  removendo o delete cascade
                
        }
    }
}

