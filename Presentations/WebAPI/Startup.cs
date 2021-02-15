using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
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
            services.AddControllers();

            services.AddScoped<IBrandDal, EfBrandDal>();
            services.AddScoped<ICarDal, EfCarDal>();
            services.AddScoped<IColorDal, EfColorDal>();
            services.AddScoped<ICustomerDal, EfCustomerDal>();
            services.AddScoped<IRentalDal, EfRentalDal>();
            services.AddScoped<IUserDal, EfUserDal>();

            services.AddScoped<IBrandService, BrandManager>();
            services.AddScoped<ICarService, CarManager>();
            services.AddScoped<IColorService, ColorManager>();
            services.AddScoped<ICustomerService, CustomerManager>();
            services.AddScoped<IRentalService, RentalManager>();
            services.AddScoped<IUserService, UserManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
