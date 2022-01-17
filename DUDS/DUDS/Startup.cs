using DUDS.Service;
using DUDS.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace DUDS
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
            // services.AddDbContext<Data.DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DahliaDatabaseContext")));
            // services.AddControllers();
            
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            /*
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
               };
           });
            */

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            //services.AddSwaggerGen(options =>
            //{
            //    options.IncludeXmlComments(xmlFilePath);
            //});

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                         builder =>
                         {
                             builder.AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowAnyOrigin()
                                    .AllowCredentials();
                         }));

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                //c.IncludeXmlComments(xmlFilePath);
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dahlia Unified Data Service", Version = "v1" });
                //c.SwaggerDoc("common", new OpenApiInfo { Title = "Dahlia Unified Data Service - Common", Version = "Common" });

                /*
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
                */
            });
            services.AddSwaggerGenNewtonsoftSupport();

            #region Service Block
            services.AddScoped<IContaService, ContaService>();
            services.AddScoped<IFundoService, FundoService>();
            services.AddScoped<IGestorService, GestorService>();
            services.AddScoped<IContratoService, ContratoService>();
            services.AddScoped<ILogErrosService, LogErrosService>();
            services.AddScoped<ITipoContaService, TipoContaService>();
            services.AddScoped<IInvestidorService, InvestidorService>();
            services.AddScoped<ICustodianteService, CustodianteService>();
            services.AddScoped<IGrupoRebateService, GrupoRebateService>();
            services.AddScoped<ISubContratoService, SubContratoService>();
            services.AddScoped<IDistribuidorService, DistribuidorService>();
            services.AddScoped<ITipoCondicaoService, TipoCondicaoService>();
            services.AddScoped<ITipoContratoService, TipoContratoService>();
            services.AddScoped<IOrdemPassivoService, OrdemPassivoService>();
            services.AddScoped<IConfiguracaoService, ConfiguracaoService>();
            services.AddScoped<ICalculoRebateService, CalculoRebateService>();
            services.AddScoped<IAdministradorService, AdministradorService>();
            services.AddScoped<IContratoFundoService, ContratoFundoService>();
            services.AddScoped<ITipoEstrategiaService, TipoEstrategiaService>();
            services.AddScoped<IControleRebateService, ControleRebateService>();
            services.AddScoped<IErrosPagamentoService, ErrosPagamentoService>();
            services.AddScoped<IContratoAlocadorService, ContratoAlocadorService>();
            services.AddScoped<IPgtoServicoService, PgtoServicoService>();
            services.AddScoped<IEmailGrupoRebateService, EmailGrupoRebateService>();
            services.AddScoped<ITipoClassificacaoService, TipoClassificacaoService>();
            services.AddScoped<IMovimentacaoPassivoService, MovimentacaoPassivoService>();
            services.AddScoped<ICondicaoRemuneracaoService, CondicaoRemuneracaoService>();
            services.AddScoped<IContratoRemuneracaoService, ContratoRemuneracaoService>();
            services.AddScoped<IPosicaoClienteService, PosicaoClienteService>();
            services.AddScoped<IPosicaoClienteService, PosicaoClienteService>();
            services.AddScoped<IInvestidorDistribuidorService, InvestidorDistribuidorService>();
            services.AddScoped<IDistribuidorAdministradorService, DistribuidorAdministradorService>();
            services.AddScoped<IPgtoTaxaAdmPfeeService, PgtoTaxaAdmPfeeService>();
            #endregion
            // Teste - Body Length
            //services.Configure<FormOptions>(options =>
            //{
            // Set the limit to 256 MB
            //    options.MultipartBodyLengthLimit = 268435456;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(options =>
                {
                    options.PreSerializeFilters.Add((swagger, req) =>
                    {
                        swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"https://{req.Host}" } };
                    });
                });
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dahlia Unified Data Service v1"));
                app.UseSwaggerUI(options =>
                {
                    //options.ConfigObject.AdditionalItems.Add("syntaxHighlight", false); //Turns off syntax highlight which causing performance issues...
                    //options.ConfigObject.AdditionalItems.Add("theme", "agate"); //Reverts Swagger UI 2.x  theme which is simpler not much performance benefit...
                    foreach (var desc in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"../swagger/{desc.GroupName}/swagger.json", "DUDS v" + desc.ApiVersion.ToString().Split(".")[0]);//desc.ApiVersion.ToString());
                        options.DefaultModelsExpandDepth(-1);
                        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                        
                    }
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
