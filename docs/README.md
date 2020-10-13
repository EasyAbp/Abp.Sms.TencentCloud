# Abp.Sms.TencentCloud

[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.Sms.TencentCloud.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.Sms.TencentCloud)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.Sms.TencentCloud.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.Sms.TencentCloud)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/Abp.Sms.TencentCloud?style=social)](https://www.github.com/EasyAbp/Abp.Sms.TencentCloud)

Abp TencentCloud SMS module.

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/How-To.md#add-nuget-packages))

    * EasyAbp.Abp.Sms.TencentCloud

1. Add `DependsOn(typeof(AbpSmsTencentCloudModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/How-To.md#add-module-dependencies))

## Usage

1. Configure by AbpTencentCloudCommonOptions and AbpTencentCloudSmsOptions.

    ```csharp
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysAllowAuthorization();
        
        Configure<AbpTencentCloudCommonOptions>(op =>
        {
            op.SecretId = "pUqsqkj1EhAYJXbSjRupWDviPAMyEaSrH5vY";
            op.SecretKey = "20DDOExLNQCTRcsogtP9AEQHI1Tcnu5R";
        });
        
        Configure<AbpTencentCloudSmsOptions>(op =>
        {
            op.DefaultSmsSdkAppid = "1400000000";
            op.DefaultSign = "多态科技";
        });
    }
    ```

2. Try to send a SMS message.

    ```csharp
    var code = "123456";    // The generated verification code
    var templateId = "400000";  // TencentCloud SMS template ID
    var phoneNumber = "+8613000000000";

    var smsSender = ServiceProvider.GetRequiredService<ISmsSender>();

    // The "text" param has no effect in TencentCloud SMS, but it cannot be null or empty.
    const string text = "placeholder";
    var smsMessage = new SmsMessage(phoneNumber, text);
    smsMessage.Properties.Add(AbpSmsTencentCloudConsts.TemplateIdPropertyName, templateId);
    smsMessage.Properties.Add(AbpSmsTencentCloudConsts.TemplateParamSetPropertyName, new [] {code});
    
    await smsSender.SendAsync(smsMessage);
    ```
