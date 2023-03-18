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
            var phoneNumber = smsMessage.PhoneNumber.StartsWith("1") && smsMessage.PhoneNumber.Length == 11
                ? $"+86{smsMessage.PhoneNumber}"
                : smsMessage.PhoneNumber;

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
                SecretKey = await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.DefaultSecretKey),
                Region = await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.DefaultRegion)
            };

            var response = await _requester.SendRequestAsync<SendSmsResponse>(request,
                await _settingProvider.GetOrNullAsync(AbpSmsTencentCloudSettings.EndPoint), commonOptions);

            if (response.Error != null)
            {
                throw new FailedToSendSmsException(response.Error.Code, response.Error.Message);
            }

            if (!response.SendStatusSet.IsNullOrEmpty() && response.SendStatusSet.First().Code != "Ok")
            {
                var firstStatus = response.SendStatusSet.First();

                throw new FailedToSendSmsException(firstStatus.Code, firstStatus.Message);
            }
        }

        protected virtual string GetStringProperty(SmsMessage smsMessage, string key, string defaultValue = null)
        {
            var str = smsMessage.Properties.GetOrDefault(key) as string;

            return !str.IsNullOrEmpty() ? str : defaultValue;
        }

        protected virtual string[] GetTemplateParamSet(SmsMessage smsMessage)
        {
            var obj = smsMessage.Properties.GetOrDefault(AbpSmsTencentCloudConsts.TemplateParamSetPropertyName);

            try
            {
                return obj switch
                {
                    null => null,
                    string str => _jsonSerializer.Deserialize<string[]>(str),
                    IEnumerable<string> set => set.ToArray(),
                    _ => _jsonSerializer.Deserialize<string[]>(obj.ToString())
                };
            }
            catch
            {
                throw new InvalidTemplateParamSetException();
            }
        }
    }
}