using System;
using System.Text.RegularExpressions;
using HealthExaminationSystem.Enumerations.Models;

namespace HealthExaminationSystem.Enumerations.Helpers
{
    /// <summary>
    /// 验证帮助器
    /// </summary>
    public static class VerificationHelper
    {
        /// <summary>
        /// 获取验证 IPv4 的正则表达式
        /// </summary>
        public static string RegexIPv4 => @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

        /// <summary>
        /// 获取验证包含 IPv4 地址的字符串的正则表达式
        /// </summary>
        public static string RegexIpv4InStr => @"\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}";

        /// <summary>
        /// 验证身份证（15位/18位）的有效性
        /// </summary>
        /// <param name="id">身份证号</param>
        /// <returns>验证成功则为 True，否则为 False。</returns>
        public static bool IdCard(string id)
        {
            switch (id.Length)
            {
                case 18:
                    return CheckIdCard18(id);
                case 15:
                    return CheckIdCard15(id);
            }

            return false;
        }

        /// <summary>
        /// 使用身份证获取生日
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null 表示身份证不合法，否则返回生日</returns>
        public static DateTime? GetBirthdayByIdCard(string id)
        {
            if (IdCard(id))
            {
                var birth = id.Length == 18 ? id.Substring(6, 8).Insert(6, "-").Insert(4, "-") : $"19{id.Substring(6, 6).Insert(4, "-").Insert(2, "-")}";
                if (DateTime.TryParse(birth, out var time))
                {
                    return time;
                }
            }

            return null;
        }

        /// <summary>
        /// 使用身份证获取性别
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null 表示身份证不合法，True 表示男性，False 表示女性</returns>
        public static bool? IsManByIdCard(string id)
        {
            if (IdCard(id))
            {
                var sex = id.Length == 18 ? id[16] : id[14];
                return sex % 2 != 0;
            }

            return null;
        }

        /// <summary>
        /// 使用身份证获取生日/年龄/性别
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null 表示身份证不合法，否则返回对象</returns>
        public static BirthdayAgeSex GetByIdCard(string id)
        {
            if (IdCard(id))
            {
                var result = new BirthdayAgeSex();
                var sex = id.Length == 18 ? id[16] : id[14];
                result.Sex = sex % 2 != 0 ? Sex.Man : Sex.Woman;
                var birth = id.Length == 18 ? id.Substring(6, 8).Insert(6, "-").Insert(4, "-") : $"19{id.Substring(6, 6).Insert(4, "-").Insert(2, "-")}";
                if (DateTime.TryParse(birth, out var time))
                {
                    result.Birthday = time;
                }
                //result.Age = DateTime.Now.Year - time.Year;               


                DateTime nowTime = DateTime.Now;
               
                int age = 0;
                if (nowTime.Month > time.Month)
                    age = nowTime.Year - time.Year;
                else if (nowTime.Month == time.Month && nowTime.Day >= time.Day)
                    age = nowTime.Year - time.Year;
                else
                    age = nowTime.Year - time.Year - 1;                 
                    result.Age =  age;
                return result;
            }

            return null;
        }

        private static bool CheckIdCard18(string id)
        {
            long n;
            if (long.TryParse(id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            var address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false;//省份验证
            }
            var birth = id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            if (DateTime.TryParse(birth, out _) == false)
            {
                return false;//生日验证
            }
            var arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            var wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            var ai = id.Remove(17).ToCharArray();
            var sum = 0;
            for (var i = 0; i < 17; i++)
            {
                sum += int.Parse(wi[i]) * int.Parse(ai[i].ToString());
            }
            int y;
            Math.DivRem(sum, 11, out y);
            if (id.Substring(0, 3).ToLower() != "320")
            {
                if (arrVarifyCode[y] != id.Substring(17, 1).ToLower())
                {
                    return false;//校验码验证
                }
            }
            return true;//符合GB11643-1999标准
        }

        private static bool CheckIdCard15(string id)
        {
            long n;
            if (long.TryParse(id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            var address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(id.Remove(2), StringComparison.Ordinal) == -1)
            {
                return false;//省份验证
            }
            var birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time;
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 验证给定的字符串是否是 IPv4地址
        /// </summary>
        /// <param name="ip">要验证的字符串</param>
        /// <returns>是则返回 True，否则为 False。</returns>
        public static bool IsIpv4Address(string ip)
        {
            var result = Regex.IsMatch(ip, RegexIPv4);
            return result;
        }

        /// <summary>
        /// 获取包含在字符串中的 IPv4 地址
        /// </summary>
        /// <param name="ipStr">包含 IPv4 地址的字符串</param>
        /// <returns>获取到的 IPv4 地址</returns>
        public static string GetIpv4Address(string ipStr)
        {
            var result = Regex.Match(ipStr, RegexIpv4InStr);
            return result.Value;
        }

        /// <summary>
        /// 替换字符串中包含的 IPv4 地址
        /// </summary>
        /// <param name="ipStr">需要替换的字符串</param>
        /// <param name="ip">新的 IPv4 地址</param>
        /// <param name="newIpStr">替换后的字符串</param>
        /// <returns>True  替换成功，否则为 False。</returns>
        public static bool ReplaceIpv4Address(string ipStr, string ip, out string newIpStr)
        {
            newIpStr = string.Empty;
            if (!IsIpv4Address(ip))
                return false;
            try
            {
                newIpStr = Regex.Replace(ipStr, RegexIpv4InStr, ip);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 验证字符串是否是空的或全部是空白字符组成
        /// </summary>
        /// <param name="value">要验证的字符串</param>
        /// <returns>如果是 null 或全部是空白组成，则为True，否则为 False。</returns>
        public static bool IsNullOrWhiteSpace(string value)
        {
            return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
        }
    }
}