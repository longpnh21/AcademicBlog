using Application.Handlers.Blogs;
using Application.Handlers.Tags;
using Application.Interfaces;
using Application.Services;
using Core.Entities;
using Core.Repositories;
using Core.Repositories.Base;
using EntityFrameworkCore.UseRowNumberForPaging;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;

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

            services.AddControllers();

            
            services.AddDbContext<AcademicBlogContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(AcademicBlogContext).Assembly.FullName);
                        builder.UseRowNumberForPaging();
                    });
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AcademicBlogContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<AcademicBlogContext>();

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(CreateBlogHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateTagHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(EditTagHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteTagHandler).GetTypeInfo().Assembly);

            services.AddTransient<IUploadService, FirebaseUploadService>();

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ITagRepository, TagRepository>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
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
