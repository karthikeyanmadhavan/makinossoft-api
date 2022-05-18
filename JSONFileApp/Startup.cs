using Common;
using DataAccess;
using DataAccess.Data;
using IService;
using JSONFileAPI.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.IO;

namespace JSONFileAPI
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
            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return ApiValidationFilter.CustomResponse(actionContext);
                };
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "File Store API",
                    Description = "Designed by: Karthikeyan Madhavan"
                });
            });
            services.AddControllers();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });
            services.AddScoped<IFileAccess, DataAccess.Data.FileAccess>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();

            LoadApplicationMessages();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new ApiExceptionHandler(env, Configuration, app.ApplicationServices.GetService<ILoggerFactory>()).Invoke
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        private void LoadApplicationMessages()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "apiresponsemessages.json");
            var JSON = System.IO.File.ReadAllText(filePath);
            MemCache.AppMessages = JsonConvert.DeserializeObject<Dictionary<int, string>>(JSON);
        }
    }
}
