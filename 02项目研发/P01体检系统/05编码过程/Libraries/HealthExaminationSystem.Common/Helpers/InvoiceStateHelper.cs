using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class InvoiceStateHelper
    {
        public static List<InvoiceStateModel> GetInvoiceStateModels()
        {
            var result = new List<InvoiceStateModel>();
            var Enable = new InvoiceStateModel
            {
                Id = (int)InvoiceState.Enable,
                Name = InvoiceState.Enable.ToString(),
                Display="启用"
            };
            result.Add(Enable);
            var Discontinuation = new InvoiceStateModel
            {
                Id = (int)InvoiceState.Discontinuation,
                Name = InvoiceState.Discontinuation.ToString(),
                Display = "停用"
            };
            result.Add(Discontinuation);
            return result;
        }
        public static List<InvoiceStateModel> GetAdStateModels()
        {
            var result = new List<InvoiceStateModel>();
            var Enable = new InvoiceStateModel
            {
                Id = 1,
                Name = InvoiceState.Enable.ToString(),
                Display = "启用"
            };
            result.Add(Enable);
            var Discontinuation = new InvoiceStateModel
            {
                Id = 0,
                Name = InvoiceState.Discontinuation.ToString(),
                Display = "停用"
            };
            result.Add(Discontinuation);
            return result;
        }
        public static List<InvoiceStateModel> GetAllInvoiceStateModels()
        {
            var result = new List<InvoiceStateModel>();
            var Enable = new InvoiceStateModel
            {
                Id = (int)InvoiceState.Enable,
                Name = InvoiceState.Enable.ToString(),
                Display = "未注销"
            };
            result.Add(Enable);
            var Discontinuation = new InvoiceStateModel
            {
                Id = 0,
                Name = InvoiceState.Discontinuation.ToString(),
                Display = "已注销"
            };
            result.Add(Discontinuation);
            var aDiscontinuation = new InvoiceStateModel
            {
                Id = 2,
                Name = "All",
                Display = "全部"
            };
            result.Add(aDiscontinuation);
            return result;
        }
    }
}
