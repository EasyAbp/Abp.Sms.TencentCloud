using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.Abp.Sms.TencentCloud.Localization;
using EasyAbp.Abp.TencentCloud.Sms;
using Volo.Abp.Json;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Sms;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    [DependsOn(
        typeof(AbpJsonModule),
        typeof(AbpSmsModule),
        typeof(AbpValidationModule),
        typeof(AbpTencentCloudSmsModule)
    )]
    public class AbpSmsTencentCloudModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpSmsTencentCloudModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AbpSmsTencentCloudResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("EasyAbp/Abp/Sms/TencentCloud/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.Abp.Sms.TencentCloud", typeof(AbpSmsTencentCloudResource));
            });
        }
    }
}
