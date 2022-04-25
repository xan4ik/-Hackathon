using WebAPI.DataProviders;
using WebAPI.DataProviders.ProvidersCSV;
using WebAPI.Services;
using Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAPI.Controllers;

namespace WebAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IDataProvider<DataEntity>, DataEntityGoogleProvider>();
            services.AddSingleton<IDataProvider<TimeEntity>, TimeEntityCsvProvider>();
            services.AddSingleton<IDataProvider<IonInfo>, IonInfoCsvProvider>();

            //services.AddTransient<IDataEntityService, DataEntityService>();
            services.AddTransient<ITimeEntityService, TimeEntityService>();
            services.AddTransient<IIonInfoService, IonInfoServices>();

            services.AddSwaggerGen();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("Human", policy =>
                {
                    policy.RequireClaim("ProfileType", AuthController.HumanRole);
                });
            });


            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();    
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();    
            });
        }
    }
}
