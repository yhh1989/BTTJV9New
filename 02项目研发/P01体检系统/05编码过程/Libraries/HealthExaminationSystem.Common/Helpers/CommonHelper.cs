using System;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 共用帮助器
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// 设置助记码
        /// </summary>
        /// <param name="helpChar">助记码文本框</param>
        /// <param name="source">源文本框</param>
        /// <param name="always">如果为 True，则总是设置助记码，否则只有在没有助记码的情况下才设置助记码</param>
        public static void SetHelpChar(TextEdit helpChar, TextEdit source, bool always = false)
        {
            var sourceString = source.Text.Trim();
            if (string.IsNullOrWhiteSpace(sourceString))
            {
                helpChar.Text = string.Empty;
                return;
            }

            if (!always && !string.IsNullOrWhiteSpace(helpChar.Text))
                return;

            try
            {
                var result = DefinedCacheHelper.DefinedApiProxy.CommonAppService.GetHansBrief(new ChineseDto { Hans = sourceString });
                helpChar.Text = result.Brief;
            }
            catch (UserFriendlyException exception)
            {
                Console.WriteLine(exception);
            }
        }

        /// <summary>
        /// 金额转换为大写形式
        /// </summary>
        /// <param name="money">金额</param>
        /// <returns></returns>
        public static string ConvertToChinese(double money)
        {
            //个、十、百、千、万、亿、兆、京、垓、秭、穰、沟、涧、正、载
            //他们的数值
            //个、
            //十、数字后1个0
            //百、数字后2个0
            //千、数字后3个0
            //万、数字后4个0
            //亿、数字后8个0
            //兆、数字后12个0
            //京、数字后16个0
            //垓、数字后20个0
            //秭、数字后24个0
            //穰、数字后28个0
            //沟、数字后32个0
            //涧、数字后36个0
            //正、数字后40个0
            //载、数字后44个0 
            var formatMoney = money.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A").Replace("0B0A", "@");
            var regexMoney = Regex.Replace(formatMoney, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            return Regex.Replace(money.Equals(0) ? "0.@" : money > 0 ? regexMoney : $"-{regexMoney}", ".", m => "负圆空零壹贰叁肆伍陆柒捌玖空空空空空空整分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
        }
        /// <summary>
        /// 根据str数组字符串获取最大数 
        /// </summary>
        /// <param name="str">string str = "1,21,3";</param>
        /// <returns>最大数</returns>
        public static double GetMaxNumByString(string str)
        {
            string[] strArr = str.Split(',');
            double[] intArr = Array.ConvertAll(strArr, new Converter<string, double>(s2i));
            Array.Sort(intArr);
            Array.Reverse(intArr);
            return intArr[0];
        }
        private static double s2i(string str)
        {
            double i = 0;
            double.TryParse(str, out i);
            return i;
        }
        /// <summary>
        /// 根据str数组字符串获取最小数 
        /// </summary>
        /// <param name="str">string str = "1,21,3";</param>
        /// <returns>最小数</returns>
        public static double GetMinNumByString(string str)
        {
            string[] strArr = str.Split(',');
            double[] intArr = Array.ConvertAll(strArr, new Converter<string, double>(s2i));
            Array.Sort(intArr);
            Array.Reverse(intArr);
            return intArr[intArr.Length - 1];
        }
        /// <summary>
        /// 字符传保留位数
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="num">保留几位</param>
        /// <returns></returns>
        public static string decimalLocation(double value, int num)
        {
            string str = string.Format("{0:N" + num.ToString() + "}", value);
            return str;
        }
    }
}