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
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        //TPH
        //public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
        //{
        //    public void Configure(EntityTypeBuilder<Pessoa> builder)
        //    {
        //        builder
        //            .ToTable("Pessoa")
        //            .HasDiscriminator<int>("TipoPessoa")
        //            .HasValue<Pessoa>(3)
        //            .HasValue<Instrutor>(6)
        //            .HasValue<Aluno>(99);
        //    }
        //}

        // TPT TABELAS POR TIPO  BOA PATRICA
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder
                .ToTable("Pessoa");
                                
        }
    }

    public class InstrutorConfiguration : IEntityTypeConfiguration<Instrutor>
    {
        public void Configure(EntityTypeBuilder<Instrutor> builder)
        {
            builder
                .ToTable("Instrutores");
                
        }
    }


    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder
                .ToTable("Alunos");
               
        }
    }
}

