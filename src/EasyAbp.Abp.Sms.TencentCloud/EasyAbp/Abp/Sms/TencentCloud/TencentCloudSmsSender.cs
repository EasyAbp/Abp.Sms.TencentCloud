using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.TencentCloud.Common.Requester;
using EasyAbp.Abp.TencentCloud.Sms;
using EasyAbp.Abp.TencentCloud.Sms.SendSms;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.Sms;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class TencentCloudSmsSender : ISmsSender, ITransientDependency
    {
        private readonly AbpTencentCloudSmsOptions _options;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ITencentCloudApiRequester _requester;

        public TencentCloudSmsSender(
            IJsonSerializer jsonSerializer,
            IOptions<AbpTencentCloudSmsOptions> options,
            ITencentCloudApiRequester requester)
        {
            _options = options.Value;
            _jsonSerializer = jsonSerializer;
            _requester = requester;
        }
        
        public virtual async Task SendAsync(SmsMessage smsMessage)
        {
            await _requester.SendRequestAsync<SendSmsResponse>(
                new SendSmsRequest(
                    new []{smsMessage.PhoneNumber},
                    GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.TemplateIdPropertyName),
                    GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.SmsSdkAppidPropertyName, _options.DefaultSmsSdkAppid),
                    GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.SignPropertyName, _options.DefaultSign),
                    GetTemplateParamSet(smsMessage),
                    GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.ExtendCodePropertyName, _options.DefaultExtendCode),
                    GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.SessionContextPropertyName),
                    GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.SenderIdPropertyName, _options.DefaultSenderId)
                ),
                _options.EndPoint);
        }

        protected virtual string GetStringProperty(SmsMessage smsMessage, string key, string defaultValue = null)
        {
            return smsMessage.Properties.GetOrDefault(key) as string;
        }

        protected virtual string[] GetTemplateParamSet(SmsMessage smsMessage)
        {
            var obj = smsMessage.Properties.GetOrDefault(AbpSmsTencentCloudConsts.TemplateParamSetPropertyName);

            return obj switch
            {
                null => null,
                string str => _jsonSerializer.Deserialize<IEnumerable<string>>(str).ToArray(),
                IEnumerable<string> set => set.ToArray(),
                _ => throw new InvalidTemplateParamSetException()
            };
        }
    }
}