using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class InvoiceTypeHelper
    {
        public static List<InvoiceTypeModel> GetInvoiceTypeModels()
        {
            var result = new List<InvoiceTypeModel>();
            var Receipt = new InvoiceTypeModel
            {
                Id=(int)InvoiceType.Receipt,
                Name=InvoiceType.Receipt.ToString(),
                Display="收据"
            };
            result.Add(Receipt);
            var Invoice = new InvoiceTypeModel
            {
                Id = (int)InvoiceType.Invoice,
                Name = InvoiceType.Invoice.ToString(),
                Display = "发票"
            };
            result.Add(Invoice);
            return result;
        }
    }
}
