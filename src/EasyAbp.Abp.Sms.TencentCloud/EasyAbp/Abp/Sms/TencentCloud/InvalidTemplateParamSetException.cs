using System;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class InvalidTemplateParamSetException : ApplicationException
    {
        public InvalidTemplateParamSetException() : base("TemplateParamSet should be IEnumerable<string>.")
        {
        }
    }
}