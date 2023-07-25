using Microsoft.EntityFrameworkCore;
using apiGestionVente.Data;
namespace apiGestionVente
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var connectionString = "server=localhost;port=3306;database=gestionvente;user=root;password=";
            builder.Services.AddDbContext<GestionVenteContext>(options =>
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)))); 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}