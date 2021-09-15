using FreeCourse.Services.Catalog.Application;
using FreeCourse.Services.Catalog.Application.Category;
using FreeCourse.Services.Catalog.Application.Course;
using FreeCourse.Services.Catalog.Settings.Abstract;
using FreeCourse.Services.Catalog.Settings.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog
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

            //services.AddControllers(options =>
            //{
            //    options.Filters.Add(new AuthorizeFilter());
            //});

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FreeCourse.Services.Catalog", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddSingleton<IDatabaseSettings>(serviceProvider =>
            {
                return serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });
            services.AddTransient<ICategoryAppService, CategoryAppService>();
            services.AddTransient<ICourseAppService, CourseAppService>();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.Authority = Configuration["IdentityServerURL"];
            //    options.Audience = "resource_catalog";
            //    options.RequireHttpsMetadata = false;
            //});

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                  builder =>
                  {
                      builder.WithOrigins()
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowAnyOrigin();
                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FreeCourse.Services.Catalog v1"));
            }

            app.UseCors("MyPolicy");
            app.UseRouting();
            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
