using Api.Helpers;
using Api.Middlewares;
using Api.Services;
using Application.Handlers.Blogs;
using Application.Interfaces;
using Core.Entities;
using Core.Repositories;
using Core.Repositories.Base;
using EntityFrameworkCore.UseRowNumberForPaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

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

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            string secretKey = Configuration["AppSettings:SecretKey"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = bool.Parse(Configuration["AppSettings:ValidateIssuer"]),
                        ValidIssuer = Configuration["AppSettings:ValidIssuer"],
                        ValidateAudience = bool.Parse(Configuration["AppSettings:ValidateAudience"]),
                        ValidAudience = Configuration["AppSettings:ValidAudience"],

                        ValidateIssuerSigningKey = bool.Parse(Configuration["AppSettings:ValidateIssuerSigningKey"]),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

                        ClockSkew = TimeSpan.Zero,

                    };
                });


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

            //Google Firebase
            string rootPath;
            if (!string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("HOME")))
            {
                rootPath = Path.Combine(System.Environment.GetEnvironmentVariable("HOME"), "site", "wwwroot");
            }
            else
            {
                rootPath = ".";
            }

            string firebaseSdkPath = Path.Combine(rootPath, Configuration["Firebase:FileOptions"]);

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(firebaseSdkPath)
            });

            //Blog
            services.AddMediatR(typeof(CreateBlogHandler).GetTypeInfo().Assembly);

            //Service
            services.AddTransient<IUploadService, FirebaseUploadService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //Repository
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AcademicBlog.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(policy => policy
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
