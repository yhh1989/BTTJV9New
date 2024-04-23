using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class PaymentTypeHelper
    {
        public static List<PaymentTypeModel> GetPaymentTypeModels()
        {
            var result = new List<PaymentTypeModel>();
            var None = new PaymentTypeModel
            {
                Id = (int)PaymentType.None,
                Name = PaymentType.None.ToString(),
                Display = "无"
            };
            result.Add(None);
            var SmallTicket = new PaymentTypeModel
            {
                Id = (int)PaymentType.SmallTicket,
                Name = PaymentType.SmallTicket.ToString(),
                Display = "小票"
            };
            result.Add(SmallTicket);
            var Receipt = new PaymentTypeModel
            {
                Id = (int)PaymentType.Receipt,
                Name = PaymentType.Receipt.ToString(),
                Display = "收据"
            };
            result.Add(Receipt);
            var Invoice = new PaymentTypeModel
            {
                Id = (int)PaymentType.Invoice,
                Name = PaymentType.Invoice.ToString(),
                Display = "发票"
            };
            result.Add(Invoice);
            return result;
        }
    }
}