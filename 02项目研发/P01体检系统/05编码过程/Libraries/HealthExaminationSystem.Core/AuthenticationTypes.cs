namespace Sw.Hospital.HealthExaminationSystem.Core
{
    /// <summary>
    /// 身份验证类型
    /// </summary>
    public static class AuthenticationTypes
    {
        /// <summary>
        /// 应用程序 Cookie
        /// </summary>
        public const string ApplicationCookie = "HealthExaminationSystemApplicationCookie";

        /// <summary>
        /// 外部 Cookie
        /// </summary>
        public const string ExternalCookie = "HealthExaminationSystemExternalCookie";

        /// <summary>
        /// 外部 Bearer
        /// </summary>
        public const string ExternalBearer = "HealthExaminationSystemExternalBearer";

        /// <summary>
        /// Two Factor Cookie
        /// </summary>
        public const string TwoFactorCookie = "HealthExaminationSystemTwoFactorCookie";

        /// <summary>
        /// Two Factor Remember Browser Cookie
        /// </summary>
        public const string TwoFactorRememberBrowserCookie = "HealthExaminationSystemTwoFactorRememberBrowser";
    }
}