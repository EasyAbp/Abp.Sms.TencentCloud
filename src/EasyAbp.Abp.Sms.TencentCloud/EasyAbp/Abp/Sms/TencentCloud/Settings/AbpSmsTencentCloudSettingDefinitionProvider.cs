using EasyAbp.Abp.Sms.TencentCloud.Localization;
using EasyAbp.Abp.TencentCloud.Common;
using EasyAbp.Abp.TencentCloud.Sms;
using Microsoft.Extensions.Options;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.Sms.TencentCloud.Settings
{
    public class AbpSmsTencentCloudSettingDefinitionProvider : SettingDefinitionProvider
    {
        private readonly AbpTencentCloudCommonOptions _commonOptions;
        private readonly AbpTencentCloudSmsOptions _smsOptions;

        public AbpSmsTencentCloudSettingDefinitionProvider(
            IOptions<AbpTencentCloudCommonOptions> commonOptions,
            IOptions<AbpTencentCloudSmsOptions> smsOptions)
        {
            _commonOptions = commonOptions.Value;
            _smsOptions = smsOptions.Value;
        }
        
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(DenturePlusSettings.MySetting1));
            
            context.Add(new SettingDefinition(
                AbpSmsTencentCloudSettings.EndPoint,
                _smsOptions.EndPoint,
                L("EndPoint")));
            
            context.Add(new SettingDefinition(
                AbpSmsTencentCloudSettings.DefaultSecretId,
                _commonOptions.SecretId,
                L("DefaultSecretId")));
            
            context.Add(new SettingDefinition(
                AbpSmsTencentCloudSettings.DefaultSecretKey,
                _commonOptions.SecretKey,
                L("DefaultSecretKey")));
            
            context.Add(new SettingDefinition(
                AbpSmsTencentCloudSettings.DefaultSmsSdkAppid,
                _smsOptions.DefaultSmsSdkAppid,
                L("DefaultSmsSdkAppid")));
            
            context.Add(new SettingDefinition(
                AbpSmsTencentCloudSettings.DefaultSign,
                _smsOptions.DefaultSign,
                L("DefaultSign")));
            
            context.Add(new SettingDefinition(
                AbpSmsTencentCloudSettings.DefaultExtendCode,
                _smsOptions.DefaultExtendCode,
                L("DefaultExtendCode")));
                        
            context.Add(new SettingDefinition(
                AbpSmsTencentCloudSettings.DefaultSenderId,
                _smsOptions.DefaultSenderId,
                L("DefaultSenderId")));

            context.Add(new SettingDefinition(
                AbpSmsTencentCloudSettings.DefaultCountryCode,
                "+86",
                L("DefaultCountryCode")));
        }
        
        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpSmsTencentCloudResource>(name);
        }
    }
}
