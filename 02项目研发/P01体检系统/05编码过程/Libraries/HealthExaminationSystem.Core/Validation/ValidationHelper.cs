using System.Text.RegularExpressions;
using Abp.Extensions;

namespace Sw.Hospital.HealthExaminationSystem.Core.Validation
{
    /// <summary>
    /// 验证帮助
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// 电子邮件正则表达式
        /// </summary>
        public const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        /// <summary>
        /// 是否是电子邮件
        /// </summary>
        /// <param name="value">待验证字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string value)
        {
            if (value.IsNullOrEmpty()) return false;

            var regex = new Regex(EmailRegex);
            return regex.IsMatch(value);
        }
    }
}