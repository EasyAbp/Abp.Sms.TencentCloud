# Abp.Sms.TencentCloud

[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.Sms.TencentCloud.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.Sms.TencentCloud)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.Sms.TencentCloud.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.Sms.TencentCloud)

Abp TencentCloud SMS module.

## Getting Started

* Install with [AbpHelper](https://github.com/EasyAbp/AbpHelper.GUI)
    Coming soon.

1. Install the following NuGet packages. (see how)
    * EasyAbp.Abp.Sms.TencentCloud

1. Add `DependsOn(typeof(xxx))` attribute to configure the module dependencies. (see how)

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

    * Send with Text.

    ```csharp
    var code = "123456";    // The generated verification code
    var templateId = "400000";  // TencentCloud SMS template ID
    var phoneNumber = "+8613000000000";

    var smsSender = ServiceProvider.GetRequiredService<ISmsSender>();

    // The "text" param should not be null or empty, but it has no effect in TencentCloud SMS.
    var smsMessage = new SmsMessage(phoneNumber, "placeholder");
    smsMessage.Properties.Add(AbpSmsTencentCloudConsts.TemplateIdPropertyName, templateId);
    smsMessage.Properties.Add(AbpSmsTencentCloudConsts.TemplateParamSetPropertyName, new [] {code});
    
    await smsSender.SendAsync(smsMessage);
    ```

    * Send with TemplateParamSet.

    ```csharp
    var code = "123456";    // The generated verification code
    var templateId = "400000";  // TencentCloud SMS template ID
    var phoneNumber = "+8613000000000";

    var smsSender = ServiceProvider.GetRequiredService<ISmsSender>();

    // The "text" param will be added to the TemplateParamSet if the latter is not set.
    var smsMessage = new SmsMessage(phoneNumber, code);
    smsMessage.Properties.Add(AbpSmsTencentCloudConsts.TemplateIdPropertyName, templateId);
    
    await smsSender.SendAsync(smsMessage);
    ```