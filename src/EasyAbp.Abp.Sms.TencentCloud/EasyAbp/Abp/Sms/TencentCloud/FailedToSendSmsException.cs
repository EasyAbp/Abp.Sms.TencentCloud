using Volo.Abp;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class FailedToSendSmsException : BusinessException
    {
        public FailedToSendSmsException(string code, string message) : base(
            $"QCloudError:{code}", $"Tencent Cloud API responses an error: [{code}] {message}.")
        {
        }
    }
}