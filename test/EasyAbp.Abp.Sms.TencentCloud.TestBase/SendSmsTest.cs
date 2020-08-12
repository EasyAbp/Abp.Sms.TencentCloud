using System.Threading.Tasks;
using EasyAbp.Abp.TencentCloud.Sms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Sms;
using Xunit;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class SendSmsTests : TencentCloudTestBase<TencentCloudTestBaseModule>
    {
        [Fact]
        public async Task SendSmsTest()
        {
            const string code = "123456";

            var smsSender = ServiceProvider.GetRequiredService<ISmsSender>();

            var smsMessage = new SmsMessage(TencentCloudTestConsts.PhoneNumber, "placeholder");
            
            smsMessage.Properties.Add(AbpSmsTencentCloudConsts.TemplateIdPropertyName, TencentCloudTestConsts.TemplateId);
            smsMessage.Properties.Add(AbpSmsTencentCloudConsts.TemplateParamSetPropertyName, new [] {code});
            
            await smsSender.SendAsync(smsMessage);
        }
    }
}