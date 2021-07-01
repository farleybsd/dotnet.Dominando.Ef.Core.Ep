using EfCore.Multitenant.Domain;
using EfCore.Multitenant.Provider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Multitenant.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }
        private readonly TenantData _tenant;
        public ApplicationContext(DbContextOptions<ApplicationContext> options, TenantData tenant) :base(options)
        {
            _tenant = tenant;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(TenantData.TenantId);

            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Name = "Person 1", TenantId = "tenant-1" },
                new Person { Id = 2, Name = "Person 2", TenantId = "tenant-2" },
                new Person { Id = 3, Name = "Person 3", TenantId = "tenant-2" });

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Description = "Description 1", TenantId = "tenant-1" },
                new Product { Id = 2, Description = "Description 2", TenantId = "tenant-2" },
                new Product { Id = 3, Description = "Description 3", TenantId = "tenant-2" });

            modelBuilder.Entity<Person>().HasQueryFilter(p=>p.TenantId == _tenant.TenantId);
            modelBuilder.Entity<Product>().HasQueryFilter(p=>p.TenantId == _tenant.TenantId);
        }
    }
}
