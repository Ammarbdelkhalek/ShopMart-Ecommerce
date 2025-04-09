
using Microsoft.Extensions.Options;
using shopMarket.Api.ActionFilter;
using shopMarket.Api.Configuration;
using shopMarket.Api.MiddelWare;

namespace shopMarket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSerilogConfiguration();
            builder.Services.AddDbContextAndIdentityServices(builder.Configuration);
            builder.Services.AddDependancyInjection(builder.Configuration);
            builder.Services.AddJwtServices(builder.Configuration);


            builder.Services.AddControllers(
                // global using 
                options => options.Filters.Add<ModelStateVlaidationActionFilter>()
            ) ;
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<CustomExceptionHandlerMiddelWare>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
