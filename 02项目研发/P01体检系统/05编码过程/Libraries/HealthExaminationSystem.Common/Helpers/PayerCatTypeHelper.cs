using System;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{

    public class PayerCatTypeHelper 
    {
        private static readonly List<PayerCatTypeModel> PayerCat = new List<PayerCatTypeModel>();
        static PayerCatTypeHelper()
        {
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.NoCharge,
                Name = PayerCatType.NoCharge.ToString(),
                Display = "未收费"
            });
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.PersonalCharge,
                Name = PayerCatType.PersonalCharge.ToString(),
                Display = "个人支付"
            });
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.ClientCharge,
                Name = PayerCatType.ClientCharge.ToString(),
                Display = "单位支付"
            });
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.MixedCharge,
                Name = PayerCatType.MixedCharge.ToString(),
                Display = "混合付款"
            });
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.GiveCharge,
                Name = PayerCatType.GiveCharge.ToString(),
                Display = "赠检"
            });
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.Charge,
                Name = PayerCatType.Charge.ToString(),
                Display = "已收费"
            });
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.Arrears,
                Name = PayerCatType.Arrears.ToString(),
                Display = "欠费"
            });
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.Refund,
                Name = PayerCatType.Refund.ToString(),
                Display = "退费"
            });
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.StayRefund,
                Name = PayerCatType.StayRefund.ToString(),
                Display = "待退费"
            });
            PayerCat.Add(new PayerCatTypeModel
            {
                Id = (int)PayerCatType.NotRefund,
                Name = PayerCatType.NotRefund.ToString(),
                Display = "无退费"
            });

        }
        /// <summary>
        /// 登记 项目/套餐 收费状态 1未收费2个人已支付3单位已支付4混合付款
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PayerCatTypeHelperFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(PayerCatType), obj))
            {
                return EnumHelper.GetEnumDesc((PayerCatType)obj);
            }
            return obj.ToString();
        }
    }

}