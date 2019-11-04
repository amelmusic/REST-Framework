using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.Core.PermissionModule.Model;
using X.Core.PermissionModule.Services;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using X.Core;
using X.Core.Interceptors;
using X.Core.Interface;
using X.Core.Services.Core.StateMachine;
using X.Core.WebAPI.Core;
using X.Core.WebAPI.Filters;

namespace X.Core.PermissionModule.API
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
            var authUrl = Configuration["Auth:Url"];
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // base-address of your identityserver
                    options.Authority = authUrl;
                    options.RequireHttpsMetadata = false;
                    // name of the API resource
                    //options.Audience = "api1";
                    options.Audience = $"{authUrl}/resources";
                });

            services.AddCors();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc(x =>
            {
                x.Filters.Add<ErrorFilter>();
                x.Filters.Add<ActionContextFilter>();
            }).AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "X.Core.PermissionModule API", Version = "v1" });
                
                //NOTE: We only ask for roles here, since permissions are bound to that
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Flow = "implicit",
                    AuthorizationUrl = authUrl + "/connect/authorize",
                    Scopes = new Dictionary<string, string> {
                        { "roles", "Access to protected resources" }
                    }
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", new[] { "roles"} }
                });
            });
            
            var connection = Configuration.GetConnectionString("X.Core.PermissionModule");
            services.AddDbContext<Services.Database.X.Core.PermissionModuleContext>(options => options.UseSqlServer(connection));
            var builder = new ContainerBuilder();

            services.AddXCore(builder);

            //we need reference so that ioc can work by default
            XCoreHelloServicesRunner servicesRunner = null;


            // Create the IServiceProvider based on the container.
            _container = builder.Build();
            return new AutofacServiceProvider(_container);
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "X.Core.PermissionModule API V1");
                c.OAuthClientId("X.Core.PermissionModule");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
