using AutoMapper;
using BookStoreManager.Entities;
using BookStoreManager.Middleware;
using BookStoreManager.Models;
using BookStoreManager.Models.Validators;
using BookStoreManager.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStoreManager
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
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddAuthentication(option =>
           {
               option.DefaultScheme = "Bearer";
               option.DefaultAuthenticateScheme = "Bearer";
               option.DefaultChallengeScheme = "Bearer";
           }).AddJwtBearer(cfg =>
           {
               cfg.RequireHttpsMetadata = true;
               cfg.SaveToken = true;
               cfg.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidIssuer = authenticationSettings.JwtIssuer,
                   ValidAudience = authenticationSettings.JwtIssuer,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
               };
           });

            services.AddControllers().AddFluentValidation();
            services.AddDbContext<BookStoreDbContext>();
            services.AddScoped<BookStoreSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<IBookStoreService, BookStoreService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<TimeMeasureMiddleware>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BookStoreSeeder seeder)
        {
            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<TimeMeasureMiddleware>();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI( c => 
            { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
