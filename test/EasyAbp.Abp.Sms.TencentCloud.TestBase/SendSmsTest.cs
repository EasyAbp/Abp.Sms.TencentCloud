using System.Threading.Tasks;
using EasyAbp.Abp.Sms.TencentCloud.Settings;
using EasyAbp.Abp.TencentCloud.Sms;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.Settings;
using Volo.Abp.Sms;
using Xunit;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class SendSmsTests : TencentCloudTestBase<TencentCloudTestBaseModule>
    {
        [Fact]
        public async Task Should_Send_Sms()
        {
            const string code = "123456";

            var smsSender = ServiceProvider.GetRequiredService<ISmsSender>();

            var smsMessage = new SmsMessage(TencentCloudTestConsts.PhoneNumber, "placeholder");
            
            smsMessage.Properties.Add(AbpSmsTencentCloudConsts.TemplateIdPropertyName, TencentCloudTestConsts.TemplateId);
            smsMessage.Properties.Add(AbpSmsTencentCloudConsts.TemplateParamSetPropertyName, new [] {code});
            
            await smsSender.SendAsync(smsMessage);
            
            // Todo
        }

        [Fact]
        public async Task Should_Get_Correct_Default_EndPoint()
        {
            var settingProvider = ServiceProvider.GetRequiredService<ISettingProvider>();

            var endPoint = await settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.EndPoint);
            
            endPoint.ShouldBe("sms.tencentcloudapi.com");
        }
    }
}