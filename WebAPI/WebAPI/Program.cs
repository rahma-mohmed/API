
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.IRepository;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<ITIContext, ITIContext>();
            builder.Services.AddScoped<IDepartment, DepartmentRepo>();
            builder.Services.AddDbContext<ITIContext>(options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("connection_string")
                ));
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ITIContext>();
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            builder.Services.AddAuthentication(
                options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(options =>
                {
                    //verified
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:IssuerIP"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:AudienceIP"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]))
                    };
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           // app.UseAuthentication(); //By Default //cookie
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
