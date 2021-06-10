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
    public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder
                .Property("_cpf")
                .HasColumnName("CPF")
                .HasMaxLength(11); // Propiedade dp modelo
                //.HasField("_cpf"); // Propiedade que ira materelizar os dados
        }
    }
}
