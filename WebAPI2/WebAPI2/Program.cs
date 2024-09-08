using WebAPI2.IRepository;
using WebAPI2.Data;
using WebAPI2.Repository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace WebAPI2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ITIContext2>(Options => Options.UseSqlServer(
                builder.Configuration.GetConnectionString("cs")
                ));
            builder.Services.AddScoped<ITIContext2,ITIContext2>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();

            //Add course Policy => Browser before go to server
            builder.Services.AddCors(options => options.AddPolicy("MyPolicy" , policy => {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            //disable filtet
            /*builder.Services.AddControllers().ConfigureApiBehaviorOptions(
                options => options.SuppressModelStateInvalidFilter = true
                );*/
            //Model.ISValid+data annotation

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

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("MyPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
