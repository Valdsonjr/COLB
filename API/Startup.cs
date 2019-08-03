using AutoMapper;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Services.Abstract;
using Services.Concrete;
using Services.Models;
using Services.Util;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;

namespace API
{
    /// <summary>
    /// Classe de Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuração
        /// </summary>
        public IConfiguration Configuration { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(opts => opts.UseSqlServer(Configuration["ConnectionString:ProBdCOLB"]));

            services.AddOData();

            services.AddMvcCore(c => 
            {
                c.EnableEndpointRouting = false;

                foreach (var formatter in c.OutputFormatters
                        .OfType<ODataOutputFormatter>()
                        .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add(
                        new MediaTypeHeaderValue("application/prs.mock-odata"));
                }
                foreach (var formatter in c.InputFormatters
                        .OfType<ODataInputFormatter>()
                        .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add(
                        new MediaTypeHeaderValue("application/prs.mock-odata"));
                }
            })
            .AddApiExplorer()
            .AddJsonFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "API COLB",
                    Version = "v1",
                    Description = "API pública do projeto de Controle de Ordens de Liberação e Branches"
                });

                //Tell Swagger to use those XML comments.
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "API.XML"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Services.XML"));
            });

            services.AddScoped<IRequisicaoService, RequisicaoService>();
            services.AddScoped<ISolucaoService, SolucaoService>();
            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddScoped<IOrdemDeLiberacaoService, OrdemDeLiberacaoService>();
            services.AddSingleton(MapperProfile.GetMapper());

            services.AddLogging(c => 
            {
                c.AddEventLog(new EventLogSettings { LogName = "COLB", SourceName = "API" });
            });
        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API COLB V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc(c => 
            {
                c.Count().Filter().OrderBy().Expand().Select().MaxTop(100);
                c.MapODataServiceRoute("odata", "odata", GetEdmModel(app.ApplicationServices));
                c.EnableDependencyInjection();
            });
        }

        private IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);

            builder.EntitySet<GetSolucaoModel>("Solucoes");
            builder.EntitySet<GetProjetoModel>("Projetos");
            builder.EntitySet<GetRequisicaoModel>("Requisicoes");
            builder.EntitySet<GetOrdemDeLiberacaoModel>("OrdensDeLiberacao");

            return builder.GetEdmModel();
        }
    }
}
