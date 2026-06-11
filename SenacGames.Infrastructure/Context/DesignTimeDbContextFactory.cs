using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.IO;

namespace SenacGames.Infrastructure.Context
{
    // Design-time factory to provide DbContext to EF tools without building the whole application.
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SenacGamesDbContext>
    {
        public SenacGamesDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile(Path.Combine("..", "SenacGames.API", "appsettings.json"), optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                   ?? "Server=(localdb)\\MSSQLLocalDB;Database=SenacGamesDb;Trusted_Connection=True;MultipleActiveResultSets=True";

            var optionsBuilder = new DbContextOptionsBuilder<SenacGamesDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SenacGamesDbContext(optionsBuilder.Options);
        }
    }
}
