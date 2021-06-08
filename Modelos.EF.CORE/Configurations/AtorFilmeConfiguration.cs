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
    //Configurando Relacionamento muitos-para-muitos
    public class AtorFilmeConfiguration : IEntityTypeConfiguration<Ator>
    {
        public void Configure(EntityTypeBuilder<Ator> builder)
        {
            //builder
            //    .HasMany(p => p.Filmes)
            //    .WithMany(p => p.Atores)
            //    .UsingEntity(p=>p.ToTable("AtoresFilmes"));

            builder
                .HasMany(p => p.Filmes)
                .WithMany(p => p.Atores)
                .UsingEntity<Dictionary<string,object>>(
                "FilmesAtores", // nome da tabela
                p=>p.HasOne<Filme>().WithMany().HasForeignKey("FilmeId"),
                p => p.HasOne<Ator>().WithMany().HasForeignKey("AtorId"),
                p =>
                {
                    p.Property<DateTime>("CadastradoEm").HasDefaultValueSql("GETDATE()");
                }
                );

        }
    }
}

