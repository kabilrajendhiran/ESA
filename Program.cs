using ESA.Data;
using TinyHelpers.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ESA.Helpers;

namespace ESA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
                    //options.JsonSerializerOptions.Converters.Add(new StringConverter());
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddDbContext<ESAContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Database=erp;Username=postgres;Password=#K@bil1998");
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //:TODO Need to enforce security rules before deploying
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}