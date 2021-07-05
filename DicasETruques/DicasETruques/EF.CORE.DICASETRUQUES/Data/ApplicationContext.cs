using EF.CORE.DICASETRUQUES.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.CORE.DICASETRUQUES.Data
{
    public class ApplicationContext :DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<UsuarioFuncao> UsuarioFuncoes { get; set; }
        public DbSet<DepartamentoRelatorio> DepartamentoRelatorio { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb; Initial Catalog=Tips;Integrated Security=true;pooling=true;")
            .LogTo(Console.WriteLine,LogLevel.Information)
            .EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeando uma View

            modelBuilder.Entity<DepartamentoRelatorio>(
                e =>
                {
                    e.HasNoKey();
                    e.ToView("vw_departamento_relatorio");
                    e.Property(p => p.Departamento).HasColumnName("Descricao");
                }
            );
        }
    }
}
