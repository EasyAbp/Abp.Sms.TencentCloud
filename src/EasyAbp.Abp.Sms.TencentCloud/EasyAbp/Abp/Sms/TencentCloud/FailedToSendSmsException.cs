using Volo.Abp;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class FailedToSendSmsException : AbpException
    {
        public FailedToSendSmsException(string code, string message) : base(
            $"Tencent Cloud API responses an error: [{code}] {message}.")
        {
        }
    }
}