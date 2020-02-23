using System;
using System.IO;
using AutoMapper;
using dotNetCore31.Business.Infrastructure.Mappings;
using dotNetCore31.Business.IServices;
using dotNetCore31.Business.Services;
using dotNetCore31.DataAccess.Infrastructure.Helpers.Connection;
using dotNetCore31.DataAccess.IRepositories;
using dotNetCore31.DataAccess.Repositories;
using dotNetCore31.WebApi.Infrastructure.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace dotNetCore31.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Connection
            services.AddTransient<IConnectionHelper, ConnectionHelper>();
            services.AddTransient<IConnectionStringHelper, ConnectionStringHelper>();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerService, CustomerService>();

            //AutoMapper 做法1:
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //AutoMapper 做法2:
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ControllerMappingProfile>();
                cfg.AddProfile<ServiceMappingProfile>();
            });
            services.AddScoped<IMapper>(s => config.CreateMapper());

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotNetCore31.WebApi", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFiles = Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);

                foreach (var xmlFile in xmlFiles)
                {
                    c.IncludeXmlComments(xmlFile);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}