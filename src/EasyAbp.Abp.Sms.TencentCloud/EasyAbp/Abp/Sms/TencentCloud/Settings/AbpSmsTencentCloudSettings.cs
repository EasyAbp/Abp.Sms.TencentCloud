namespace EasyAbp.Abp.Sms.TencentCloud.Settings
{
    public static class AbpSmsTencentCloudSettings
    {
        private const string Prefix = "EasyAbp.Abp.Sms.TencentCloud";

        //Add your own setting names here. Example:
        //public const string MySetting1 = Prefix + ".MySetting1";

        public const string EndPoint = Prefix + ".EndPoint";
        
        public const string DefaultSecretId = Prefix + ".DefaultSecretId";
        
        public const string DefaultSecretKey = Prefix + ".DefaultSecretKey";
        
        public const string DefaultSmsSdkAppid = Prefix + ".DefaultSmsSdkAppid";
        
        public const string DefaultSign = Prefix + ".DefaultSign";
        
        public const string DefaultExtendCode = Prefix + ".DefaultExtendCode";
        
        public const string DefaultSenderId = Prefix + ".DefaultSenderId";

        public const string DefaultCountryCode = Prefix + ".DefaultCountryCode";

    }
}