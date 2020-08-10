using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasyAbp.Abp.Sms.TencentCloud.EntityFrameworkCore
{
    public class TencentCloudHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<TencentCloudHttpApiHostMigrationsDbContext>
    {
        public TencentCloudHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<TencentCloudHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("TencentCloud"));

            return new TencentCloudHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
