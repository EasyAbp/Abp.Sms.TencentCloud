using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class SmsSendingException : ApplicationException
    {
        public string Code { get; private set; }
        public SmsSendingException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}