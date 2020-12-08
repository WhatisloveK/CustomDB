using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB_Engine.Extentions;
using GrpcServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrpcServer
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        protected virtual Settings Settings { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Settings = Configuration.Get<Settings>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddGrpc();
            ConfigureIoC(services);
            ConfigureCors(services);
        }

        public void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
            }));
        }

        public void ConfigureIoC(IServiceCollection services)
        {
            services.AddSingleton(x => Settings);
            services.AddCustomDbEngine();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseGrpcWeb();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<DatabaseService>().EnableGrpcWeb()
                                                  .RequireCors("AllowAll");
                endpoints.MapGrpcService<TableService>().EnableGrpcWeb()
                                                  .RequireCors("AllowAll");
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
