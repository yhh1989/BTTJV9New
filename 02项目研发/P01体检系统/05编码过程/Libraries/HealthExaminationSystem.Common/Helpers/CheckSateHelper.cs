using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System.Linq;
using System;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 体检状态帮助
    /// </summary>
    public static class CheckSateHelper
    {
        /// <summary>
        /// 体检状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PhysicalEStateFormatter(object obj)
        {
            if (obj == null)
                return string.Empty;
            if (Enum.IsDefined(typeof(PhysicalEState), obj))
            {
                return EnumHelper.GetEnumDesc((PhysicalEState)obj);
            }
            return obj.ToString();
        }

        /// <summary>
        /// 体检类型格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ExaminationFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(ExaminationCategory), obj))
            {
                return EnumHelper.GetEnumDesc((ExaminationCategory)obj);
            }
            return obj.ToString();
        }


        /// <summary>
        /// 项目检查状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ProjectIStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(ProjectIState), obj))
            {
                return EnumHelper.GetEnumDesc((ProjectIState)obj);
            }
            return obj.ToString();
        }



        /// <summary>
        /// 总检锁定状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SummLockedFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(SummLockedState), obj))
            {
                return EnumHelper.GetEnumDesc((SummLockedState)obj);
            }
            return obj.ToString();
        }



        /// <summary>
        /// 报告领取状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ReceiveSateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(ReceiveSateState), obj))
            {
                return EnumHelper.GetEnumDesc((ReceiveSateState)obj);
            }
            return obj.ToString();
        }

        /// <summary>
        /// 报告打印状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PrintSateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(PrintSate), obj))
            {
                return EnumHelper.GetEnumDesc((PrintSate)obj);
            }
            return obj.ToString();
        }

        /// <summary>
        /// 总检状态式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SummSateFormatter(object obj)
        {
            if (obj == null)
            {
                return "未总检";
            }
            if (Enum.IsDefined(typeof(SummSate), obj))
            {
                return EnumHelper.GetEnumDesc((SummSate)obj);
            }
            return obj.ToString();
        }
    }
}