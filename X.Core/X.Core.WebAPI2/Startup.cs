//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;
//using Autofac;
//using Autofac.Extensions.DependencyInjection;
//using Autofac.Extras.DynamicProxy;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Swashbuckle.AspNetCore.Swagger;
//using X.Core.Interface;
//using X.Core.Interfaces;
//using X.Core.Services;
//using X.Core.Services.Database;
//using X.Core.WebAPI.Core;
//using X.Core.WebAPI.Generators;

//[assembly: WebAPIGenerator( InterfacesPath= "../X.Core.Interfaces"
//    , WebAPINamespace = "X.Core.WebAPI"
//    , InterfacesNamespace = "X.Core.Interfaces")]
//namespace X.Core.WebAPI
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }
//        IContainer _container = null;
//        // This method gets called by the runtime. Use this method to add services to the container.
//        public IServiceProvider ConfigureServices(IServiceCollection services)
//        {
//            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
//            });

//            var connection = @"Server=.;Database=Core360_Correspondence;Trusted_Connection=True;";
//            services.AddDbContext<CorrespondenceContext>(options => options.UseSqlServer(connection));

//            services.AddXCore(ref _container);
//            // Create the IServiceProvider based on the container.
//            return new AutofacServiceProvider(_container);
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }

//            app.UseSwagger();

//            app.UseSwaggerUI(c =>
//            {
//                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

//            });

//            app.UseMvc();
//        }
//    }
//}
