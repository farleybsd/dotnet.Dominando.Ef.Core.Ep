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
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //Owned Types Tipos Complexos
            builder.OwnsOne(x => x.Endereco, end =>
                {
                    end.Property(p => p.Bairro).HasColumnName("Bairro"); // Criando o nome na tabela do db
                    end.ToTable("Endereco");
                });

        }
    }
}

