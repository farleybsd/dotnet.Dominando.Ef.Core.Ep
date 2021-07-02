using Api.UoW.Repository.Data;
using Api.UoW.Repository.Data.Repositories;
using Api.UoW.Repository.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.UoW.Repository
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //NewtonsoftJson
            services.AddControllers()
                .AddNewtonsoftJson(options =>{
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            //Swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EFCore.UowRepository", Version = "v1" });
            });
            //Registrando Contexto
            services.AddDbContext<ApplicationContext>(p=>p.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=UoW; Integrated Security=true"));
            // Registrando Interface Repository
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCore.UowRepository v1"));
            }

            InicializarBaseDeDados(app);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void InicializarBaseDeDados(IApplicationBuilder app)
        {
            using var db = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<ApplicationContext>();

            if (db.Database.EnsureCreated())
            {
                db.Departamentos.AddRange(Enumerable.Range(1, 10)
                    .Select(p => new Departamento
                    {
                        Descricao = $"Departamento - {p}",
                        Colaboradores = Enumerable.Range(1, 10)
                            .Select(x => new Colaborador
                            {
                                Nome = $"Colaborador: {x}/{p}"
                            }).ToList()
                    }));

                db.SaveChanges();
            }
        }
    }
}

