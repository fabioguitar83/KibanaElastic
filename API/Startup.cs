
using FluentValidation;
using KibanaSerilog.Application.CommandHandlers;
using KibanaSerilog.Domain.Behaviors;
using KibanaSerilog.Infrastructure.Ioc;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace KibanaSerilog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //ELASTIC
            var elasticUri = Configuration["ElasticConfiguration:Uri"];
            var indexPatern = Configuration["ElasticConfiguration:IndexPatern"];

            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
               {
                   AutoRegisterTemplate = true,
                   IndexFormat = indexPatern
               })
           .CreateLogger();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //SWAGGER
            services.AddSwaggerGen();

            //IOC
            DependencyInjection.Build(services);

            //MEDIATR
            AddMediatr(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //SERILOG
            loggerFactory.AddSerilog();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddMediatr(IServiceCollection services)
        {
            var applicationAssemblyName = "KibanaSerilog.Domain";
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddMediatR(typeof(Startup), typeof(UserHandler));
        }
    }
}
