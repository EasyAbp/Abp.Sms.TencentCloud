using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.Abp.Sms.TencentCloud.EntityFrameworkCore
{
    public class TencentCloudHttpApiHostMigrationsDbContext : AbpDbContext<TencentCloudHttpApiHostMigrationsDbContext>
    {
        public TencentCloudHttpApiHostMigrationsDbContext(DbContextOptions<TencentCloudHttpApiHostMigrationsDbContext> options)
            : base(options)
        {
        
        }
    }
}
