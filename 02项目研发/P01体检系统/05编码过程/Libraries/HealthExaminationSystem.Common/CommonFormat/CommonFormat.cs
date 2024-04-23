﻿using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.CommonFormat
{
    public static class CommonFormat
    {
        public static string YesOrNoFormatter(object obj)
        {
            return (int.TryParse(obj?.ToString(), out int val) && val == 1) ? "是" : "否";
        }

        /// <summary>
        /// 样本类型格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SpecimenTypeFormatter(object obj)
        {
            if (obj != null)
            {
                var bd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.SpecimenType, (int)obj);
                if (bd != null) return bd.Text;
            }
            return obj.ToString();
        }

        /// <summary>
        /// 体检类型格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ExaminationTypeFormatter(object obj)
        {
            if (obj != null)
            {
                var bd = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.ExaminationType, (int)obj);
                if (bd != null) return bd.Text;
            }
            return obj.ToString();
        }

        /// <summary>
        /// 套餐类型格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ItemSuitTypeFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(ItemSuitType), obj))
            {
                return EnumHelper.GetEnumDesc((ItemSuitType)obj);
            }
            return obj.ToString();
        }
        /// <summary>
        /// 阿拉伯数字转换成中文数字
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string NumToChinese(string x)
        {
            string[] pArrayNum = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            //为数字位数建立一个位数组
            string[] pArrayDigit = { "", "十", "百", "千" };
            //为数字单位建立一个单位数组
            string[] pArrayUnits = { "", "万", "亿", "万亿" };
            var pStrReturnValue = ""; //返回值
            var finger = 0; //字符位置指针
            var pIntM = x.Length % 4; //取模
            int pIntK;
            if (pIntM > 0)
                pIntK = x.Length / 4 + 1;
            else
                pIntK = x.Length / 4;
            //外层循环,四位一组,每组最后加上单位: ",万亿,",",亿,",",万,"
            for (var i = pIntK; i > 0; i--)
            {
                var pIntL = 4;
                if (i == pIntK && pIntM != 0)
                    pIntL = pIntM;
                //得到一组四位数
                var four = x.Substring(finger, pIntL);
                var P_int_l = four.Length;
                //内层循环在该组中的每一位数上循环
                for (int j = 0; j < P_int_l; j++)
                {
                    //处理组中的每一位数加上所在的位
                    int n = Convert.ToInt32(four.Substring(j, 1));
                    if (n == 0)
                    {
                        if (j < P_int_l - 1 && Convert.ToInt32(four.Substring(j + 1, 1)) > 0 && !pStrReturnValue.EndsWith(pArrayNum[n]))
                            pStrReturnValue += pArrayNum[n];
                    }
                    else
                    {
                        if (!(n == 1 && (pStrReturnValue.EndsWith(pArrayNum[0]) | pStrReturnValue.Length == 0) && j == P_int_l - 2))
                            pStrReturnValue += pArrayNum[n];
                        pStrReturnValue += pArrayDigit[P_int_l - j - 1];
                    }
                }
                finger += pIntL;
                //每组最后加上一个单位:",万,",",亿," 等
                if (i < pIntK) //如果不是最高位的一组
                {
                    if (Convert.ToInt32(four) != 0)
                        //如果所有4位不全是0则加上单位",万,",",亿,"等
                        pStrReturnValue += pArrayUnits[i - 1];
                }
                else
                {
                    //处理最高位的一组,最后必须加上单位
                    pStrReturnValue += pArrayUnits[i - 1];
                }
            }
            return pStrReturnValue;
        }

    }
}
