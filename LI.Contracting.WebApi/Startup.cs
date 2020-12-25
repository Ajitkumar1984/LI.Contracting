using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LI.Contracting.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LI.Contracting.WebApi
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
           
            string st = Configuration.GetConnectionString("DefaultConnection");
            string AllowedHosts = Configuration.GetSection("AllowedHosts").Value;
            services.AddDbContext<ContractingContext>(options =>options.UseSqlServer(st));
            services.AddScoped<ContractingContext>();
            services.AddTransient(typeof(IContractDataManager<>), typeof(ContractDataManager<>));
            services.AddAutoMapper(typeof(ModelMapper));

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CORSPolicy",
                    builder =>
                    {
                        builder.WithOrigins(AllowedHosts).AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                    });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors("CORSPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
