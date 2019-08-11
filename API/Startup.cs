using DataAccess;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Services.Abstract;
using Services.Concrete;
using Services.Models;
using Services.Util;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
            .AddAuthorization()
            .AddJsonFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<AppSettings>(Configuration.GetSection("Keys"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "API COLB",
                    Version = "v1",
                    Description = "API pública do projeto de Controle de Ordens de Liberação e Branches"
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Header JWT",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });

                c.MapType<PostSessaoModel>(() => new Schema
                {
                    Properties = new Dictionary<string, Schema>
                    {
                        { "email", new Schema { Type = "string" } },
                        { "senha", new Schema { Type = "string" } }
                    }
                });

                c.MapType<PostUsuarioModel>(() => new Schema
                {
                    Properties = new Dictionary<string, Schema>
                    {
                        { "nmUsuario", new Schema { Type = "string" } },
                        { "dsEmail", new Schema { Type = "string" } },
                        { "nrTelefone", new Schema { Type = "integer", Format = "int64" } },
                        { "dtNascimento", new Schema { Type = "string", Format = "date-time" } },
                        { "dsSenha", new Schema { Type = "string" } }
                    }
                });

                //Tell Swagger to use those XML comments.
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "API.XML"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Services.XML"));
            });

            services.AddScoped<IRequisicaoService, RequisicaoService>();
            services.AddScoped<ISolucaoService, SolucaoService>();
            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddScoped<IOrdemDeLiberacaoService, OrdemDeLiberacaoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddSingleton(MapperProfile.GetMapper());

            services.AddLogging(c => 
            {
                c.AddEventLog(new EventLogSettings { LogName = "COLB", SourceName = "API" });
            });

            var key = Encoding.ASCII.GetBytes(Configuration["Keys:Secret"]);
            services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
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

            app.UseAuthentication();

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

            builder.EntitySet<GetSolucaoModel>("Solucoes").EntityType.HasKey(s => s.CdSolucao);
            builder.EntitySet<GetProjetoModel>("Projetos").EntityType.HasKey(p => p.CdProjeto);
            builder.EntitySet<GetRequisicaoModel>("Requisicoes").EntityType.HasKey(r => r.NrRequisicao);
            builder.EntitySet<GetOrdemDeLiberacaoModel>("OrdensDeLiberacao").EntityType.HasKey(ol => ol.NrOrdemDeLiberacao);

            return builder.GetEdmModel();
        }
    }
}
