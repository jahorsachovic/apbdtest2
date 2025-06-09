using Microsoft.EntityFrameworkCore;
using test2.DAL;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ConcertsDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
        
        
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}