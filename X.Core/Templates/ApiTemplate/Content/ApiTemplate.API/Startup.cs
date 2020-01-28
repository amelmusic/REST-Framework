using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApiTemplate.Interfaces;
using ApiTemplate.Model;
using ApiTemplate.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using X.Core;
using X.Core.Interceptors;
using X.Core.Interface;
using X.Core.Services.Core.StateMachine;
using X.Core.WebAPI.Core;
using X.Core.WebAPI.Filters;

namespace ApiTemplate.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        IContainer _container = null;
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var authUrl = Configuration["Auth:Url"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // base-address of your identityserver
                    options.Authority = authUrl;
                    options.RequireHttpsMetadata = false;
                    // name of the API resource
                    //options.Audience = "api1";
                    options.Audience = $"roles"; //TODO: Change this to your needs
                });

            services.AddCors();

            services.AddMvc(x =>
            {
                x.Filters.Add<ErrorFilter>();
                x.Filters.Add<ActionContextFilter>();
                x.Filters.Add<PermissionFilter>();
                //x.UseGeneralRoutePrefix("ApiTemplate"); //same as module name when scaffolded
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            })
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.IgnoreNullValues = true;
            //    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            //    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            //    //TODO: THIS WILL BE USABLE WHEN WE HAVE REFERENCE LOOP HANDLING
            //})
            .AddApplicationPart(typeof(PermissionCheckController).Assembly)
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiTemplate API", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(authUrl + "/connect/authorize"),
                            Scopes = new Dictionary<string, string> {
                                            { "roles", "Access to protected resources" }
                                        }
                        }
                    }
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                            },
                            new[] { "roles" }
                        }
                    });
            });
            
            var connection = Configuration.GetConnectionString("ApiTemplate");
            services.AddDbContext<Services.Database.ApiTemplateContext>(options => options.UseSqlServer(connection));

            var builder = new ContainerBuilder();

            services.AddXCore(builder);

            //we need reference so that ioc can work by default
            ApiTemplateServicesRunner servicesRunner = null;


            builder.RegisterType<SetupService>()
            .As<ISetupService>()
            .InstancePerLifetimeScope()
            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
            .EnableClassInterceptors()
            .InterceptedBy(typeof(LogInterceptorProxy))
            .InterceptedBy(typeof(CacheInterceptorProxy))
            .InterceptedBy(typeof(TransactionInterceptorProxy));

            // Create the IServiceProvider based on the container.
            _container = builder.Build();
            return new AutofacServiceProvider(_container);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiTemplate API V1");
                c.OAuthClientId("ApiTemplate");
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
