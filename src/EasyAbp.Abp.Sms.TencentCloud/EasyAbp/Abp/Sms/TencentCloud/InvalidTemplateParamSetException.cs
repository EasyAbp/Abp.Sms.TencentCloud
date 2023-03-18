using Volo.Abp;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class InvalidTemplateParamSetException : AbpException
    {
        public InvalidTemplateParamSetException() : base(
            "TemplateParamSet should be IEnumerable<string> or JSON string collection.")
        {
        }
    }
}