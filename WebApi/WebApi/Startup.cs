using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using WebApi.DataProviders;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IDataProvider<DataEntity>, DataEntityGoogleProvider>();
            services.AddSingleton<IDataProvider<TimeEntity>, TimeEntityGoogleProvider>();
            services.AddSingleton<IDataProvider<IonInfo>, IonTypeGoogleProvider>();

            services.AddTransient<IDataEntityService, DataEntityService>();
            services.AddTransient<ITimeEntityService, TimeEntityService>();
            services.AddTransient<IIonInfoService, IonInfoServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();    
            });
        }
    }
}