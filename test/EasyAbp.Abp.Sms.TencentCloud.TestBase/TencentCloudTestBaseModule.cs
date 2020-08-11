using EasyAbp.Abp.TencentCloud.Common;
using EasyAbp.Abp.TencentCloud.Sms;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpSmsTencentCloudModule)
        )]
    public class TencentCloudTestBaseModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAlwaysAllowAuthorization();
            
            Configure<AbpTencentCloudCommonOptions>(op =>
            {
                op.SecretId = TencentCloudTestConsts.SecretId;
                op.SecretKey = TencentCloudTestConsts.SecretKey;
            });
            
            Configure<AbpTencentCloudSmsOptions>(op =>
            {
                op.DefaultSmsSdkAppid = TencentCloudTestConsts.SmsSdkAppid;
                op.DefaultSign = TencentCloudTestConsts.Sign;
            });
        }
    }
}
