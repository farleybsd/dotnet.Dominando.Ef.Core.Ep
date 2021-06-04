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


        }
    }
}

