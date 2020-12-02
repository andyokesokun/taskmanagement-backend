using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AutoMapper;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Data.Seeds;
using TaskManagement.Infrastructure.Services;
using TaskManagement.Core.Map;

namespace TaskManagement.API
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
            services.AddControllers(config => {
                var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
                
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCors();
            services.AddSwaggerGen(config => {

        

                config.AddSecurityDefinition("Bearer",  new Microsoft.OpenApi.Models.OpenApiSecurityScheme { 
            
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
               
                
                });

            });
            services.AddDbContext<DataContext>(option =>
                option.UseSqlite(Configuration.GetConnectionString("Dbtest"))
            );
                    


            services.AddIdentity<AppUser, IdentityRole>(option => option.SignIn.RequireConfirmedAccount= false)
                    .AddEntityFrameworkStores<DataContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience =true,
                            ValidateLifetime =true,
                            ValidIssuer = Configuration["JWT:Issuer"],
                            ValidAudience =Configuration["JWT:Issuer"],
                            IssuerSigningKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]) )
                            
                        };
                    });

            //Applications services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskRepository, TaskService>();
            services.AddAutoMapper(x => x.AddProfile(new MappingEntity()));



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserService userService)
        {

            
            

             var x=UserSeed.CreateUsers(userService, Configuration);

             x.Wait();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


           
            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseCors(option => {
                option.SetIsOriginAllowed(origin => true)
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
                  });
                

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
