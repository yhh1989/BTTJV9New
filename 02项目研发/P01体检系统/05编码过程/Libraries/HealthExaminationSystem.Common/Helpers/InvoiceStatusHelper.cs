using System;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class InvoiceStatusHelper
    {
        private static readonly List<InvoiceStatus> CustomMarrySates = new List<InvoiceStatus>();

        /// <summary>
        /// 体检状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PayerCatInvoiceStatus(object obj)
        {
            if (Enum.IsDefined(typeof(InvoiceStatus), obj))
                return EnumHelper.GetEnumDesc((InvoiceStatus)obj);

            return obj.ToString();
        }

        public static List<InvoiceStatus> GetMarrySateModels()
        {
            return CustomMarrySates;
        }
    }
}