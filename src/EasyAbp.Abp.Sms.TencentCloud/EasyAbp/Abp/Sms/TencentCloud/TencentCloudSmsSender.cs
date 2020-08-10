using System.Threading.Tasks;
using Volo.Abp.Sms;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class TencentCloudSmsSender : ISmsSender
    {
        public virtual async Task SendAsync(SmsMessage smsMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}