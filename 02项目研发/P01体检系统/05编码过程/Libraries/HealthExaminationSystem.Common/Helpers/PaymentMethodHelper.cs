using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class PaymentMethodHelper
    {
        /// <summary>
        /// 支付方式/加项方式
        /// </summary>
        /// <returns></returns>
        public static List<PayerCatTypeModel> GetPaymentMethod()
        {
            var result = new List<PayerCatTypeModel>();
            var Company = new PayerCatTypeModel
            {
                Id = (int)PayerCatType.ClientCharge,
                Name = PayerCatType.ClientCharge.ToString(),
                Display = "单位支付"
            };
            result.Add(Company);
            var Personal = new PayerCatTypeModel
            {
                Id = (int)PayerCatType.PersonalCharge,
                Name = PayerCatType.PersonalCharge.ToString(),
                Display = "个人支付"
            };
            result.Add(Personal);
            var FixedAmount = new PayerCatTypeModel
            {
                Id = (int)PayerCatType.FixedAmount,
                Name = PayerCatType.FixedAmount.ToString(),
                Display = "限额"
            };
            result.Add(FixedAmount);
            return result;
        }
    }
}
