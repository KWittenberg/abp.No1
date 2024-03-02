using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace No1.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class No1DbContextFactory : IDesignTimeDbContextFactory<No1DbContext>
{
    public No1DbContext CreateDbContext(string[] args)
    {
        No1EfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<No1DbContext>()
            //.UseSqlServer(configuration.GetConnectionString("Default"));
            .UseSqlServer(configuration.GetConnectionString("Default"), o =>
            {
                //o.UseDateOnlyTimeOnly();
                o.UseNetTopologySuite();
            });

        return new No1DbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../No1.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}