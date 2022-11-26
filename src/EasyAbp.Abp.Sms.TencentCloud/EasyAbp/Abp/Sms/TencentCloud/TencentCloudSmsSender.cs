using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.Sms.TencentCloud.Settings;
using EasyAbp.Abp.TencentCloud.Common;
using EasyAbp.Abp.TencentCloud.Common.Requester;
using EasyAbp.Abp.TencentCloud.Sms.SendSms;
using Volo.Abp.Json;
using Volo.Abp.Settings;
using Volo.Abp.Sms;

namespace EasyAbp.Abp.Sms.TencentCloud
{
    public class TencentCloudSmsSender : ISmsSender
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ISettingProvider _settingProvider;
        private readonly ITencentCloudApiRequester _requester;

        public TencentCloudSmsSender(
            IJsonSerializer jsonSerializer,
            ISettingProvider settingProvider,
            ITencentCloudApiRequester requester)
        {
            _jsonSerializer = jsonSerializer;
            _settingProvider = settingProvider;
            _requester = requester;
        }
        
        public virtual async Task SendAsync(SmsMessage smsMessage)
        {
            var countryCode = await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.DefaultCountryCode);

            string phoneNumber = smsMessage.PhoneNumber;

            if (!countryCode.IsNullOrEmpty() && !phoneNumber.StartsWith("+"))
            {
                phoneNumber = countryCode + phoneNumber;
            }

                var request = new SendSmsRequest(
                new[] { phoneNumber },
                GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.TemplateIdPropertyName),
                GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.SmsSdkAppidPropertyName,
                    await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.DefaultSmsSdkAppid)),
                GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.SignPropertyName,
                    await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.DefaultSign)),
                GetTemplateParamSet(smsMessage),
                GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.ExtendCodePropertyName,
                    await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.DefaultExtendCode)),
                GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.SessionContextPropertyName),
                GetStringProperty(smsMessage, AbpSmsTencentCloudConsts.SenderIdPropertyName,
                    await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.DefaultSenderId))
            );
            
            var commonOptions = new AbpTencentCloudCommonOptions
            {
                SecretId = await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.DefaultSecretId),
                SecretKey = await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.DefaultSecretKey)
            };

            await _requester.SendRequestAsync<SendSmsResponse>(request,
                await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.EndPoint), commonOptions);
        }

        protected virtual string GetStringProperty(SmsMessage smsMessage, string key, string defaultValue = null)
        {
            var str = smsMessage.Properties.GetOrDefault(key) as string;
            
            return !str.IsNullOrEmpty() ? str : defaultValue;
        }

        protected virtual string[] GetTemplateParamSet(SmsMessage smsMessage)
        {
            var obj = smsMessage.Properties.GetOrDefault(AbpSmsTencentCloudConsts.TemplateParamSetPropertyName);

            return obj switch
            {
                null => null,
                string str => _jsonSerializer.Deserialize<string[]>(str),
                IEnumerable<string> set => set.ToArray(),
                _ => _jsonSerializer.Deserialize<string[]>(_jsonSerializer.Serialize(obj))
            };
        }
    }
}