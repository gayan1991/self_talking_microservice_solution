using BoilerPlate.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using BoilerPlate.Infrastructure.Repositories;
using BoilerPlate.Domain.Interfaces.Repositories;
using BoilerPlate.Service.Interfaces;
using BoilerPlate.Service.Services;
using Confluent.Kafka;
using BoilerPlate.Application.BackgroundServices;
using BoilerPlate.Application.Interceptors;
using BoilerPlate.Infrastructure.Event;
using BoilerPlate.Infrastructure.Event.Outbox;
using BoilerPlate.Application.EventHandler;
using BoilerPlate.Application.Interfaces;

namespace BoilerPlate.Application
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

            #region BackgroundSVC
                        
            services.Configure<KafkaTopics>(Configuration.GetSection("TopicsConfiguration"));
            services.Configure<KafkaConfig>(Configuration.GetSection("KafkaConfig"));
            services.AddSingleton<SaveChangesInterceptor, UserSaveChangeInterceptor>();

            #endregion

            #region EventHandlers

            services.AddScoped<IEventHandler<EventMessage>, AuditEventHandler>();

            #endregion

            var producerConfig = new ProducerConfig();
            Configuration.Bind("KafkaConfig", producerConfig);

            services.AddSingleton(producerConfig);

            services.AddControllers();

            services.AddSwaggerGen();

            #region EntityFramework Core

            services.AddEntityFrameworkSqlServer() 
             .AddDbContext<BoilerPlateDbContext>((serviceProvider, options) =>
             {
                 options.UseSqlServer(Configuration["Database:ConnectionString"], sqlServerOptionsAction: sqlOptions =>
                 {
                     sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                     sqlOptions.CommandTimeout(Configuration.GetValue("Database:CommandTimeout", 30));
                     sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                 })
                 .AddInterceptors(serviceProvider.GetRequiredService<SaveChangesInterceptor>());
             });

            services.AddEntityFrameworkSqlServer()
             .AddDbContext<AuditDbContext>((serviceProvider, options) =>
             {
                 options.UseSqlServer(Configuration["Database:ConnectionString"], sqlServerOptionsAction: sqlOptions =>
                 {
                     sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                     sqlOptions.CommandTimeout(Configuration.GetValue("Database:CommandTimeout", 30));
                     sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                 });
             });
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<ISendIntegrationEventRepository, SendIntegrationEventRepository>();
            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuditService, AuditService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //    c.RoutePrefix = string.Empty;
            //});
            //app.UseRouting();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
