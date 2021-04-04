using System;
using System.IO;
using System.Reflection;
using Api.Common;
using Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;

namespace Api
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
            services.Configure<ServiceSettings>(
                Configuration.GetSection(nameof(ServiceSettings)));
            
            services.AddControllers();
            
            services.AddScoped<IApiOneClient, ApiOneClient>();
            services.AddScoped<IFeeCalculatorService, FeeCalculatorService>();
            
            services.AddHttpClient<ApiOneClient>()
                .AddTransientHttpErrorPolicy(builder =>
                    builder.WaitAndRetryAsync(2, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Two", 
                    Version = "v1",
                    Description = "An Api to calculate fees with interests",
                    Contact = new OpenApiContact
                    {
                        Name = "Antonio Manoel",
                        Email = "antony.trino@gmail.com",
                        Url = new Uri("https://github.com/Tony0000/FeeCalculator")
                    }
                });
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}