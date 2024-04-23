using System;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class PrintSateHelper
    {
        private static readonly List<PrintSateModel> PrintSateModels = new List<PrintSateModel>();

        static PrintSateHelper()
        {
            PrintSateModels.Add(new PrintSateModel
            {
                Id = (int)PrintSate.NotToPrint,
                Name = PrintSate.NotToPrint.ToString(),
                Display = "未打印"
            });
            PrintSateModels.Add(new PrintSateModel
            {
                Id = (int)PrintSate.Print,
                Name = PrintSate.Print.ToString(),
                Display = "已打印"
            });
            PrintSateModels.Add(new PrintSateModel
            {
                Id = 3,
                Name ="AllPrint",
                Display = "全部"
            });
        }
        /// <summary>
        /// 危急值状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PrintSateFormatter(object obj)
        {
            if (obj == null)
            {
                return "未知";
            }
            if (Enum.IsDefined(typeof(PrintSate), obj))
            {
                return EnumHelper.GetEnumDesc((PrintSate)obj);
            }
            return obj.ToString();
        }
        public static List<PrintSateModel> GetSexModels()
        {
            return PrintSateModels;
        }
    }
}