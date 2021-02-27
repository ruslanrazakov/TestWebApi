using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestWebApi.Data;
using TestWebApi.Models;
using TestWebApi.Services;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.AspNetCore;
using Hangfire.MemoryStorage;

namespace TestWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("MainDatabase")));  
                services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new Converters.CustomJsonStringEnumConverter());
                });

            services.AddHangfire(config =>
            {
                config.UseMemoryStorage();
            });

            services.AddScoped<IPostOrderService, PostOrderService>();
            services.AddScoped<IOrdersProcessor, OrdersProcessor>();
            services.AddScoped<IOrdersProcessorLogger, OrdersProcessorLogger>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RecurringJob.AddOrUpdate((IOrdersProcessor o) => o.Init(), Constants.Strings.cronEveryFiveMinute);
        }
    }
}
