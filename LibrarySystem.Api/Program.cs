using LibrarySystem.Application;
using LibrarySystem.Infrastructure;

namespace LibrarySystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ═══════════════════════════════════════════════════════════════
            // APPLICATION
            // ═══════════════════════════════════════════════════════════════
            builder.Services.AddApplication();

            // ═══════════════════════════════════════════════════════════════
            // INFRASTRUCTURE
            // ═══════════════════════════════════════════════════════════════
            builder.Services.AddInfrastructure(builder.Configuration);

            // ═══════════════════════════════════════════════════════════════
            // API (Presentation)
            // ═══════════════════════════════════════════════════════════════
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ═══════════════════════════════════════════════════════════════
            // MIDDLEWARE PIPELINE
            // ═══════════════════════════════════════════════════════════════
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