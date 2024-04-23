using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class ChargeApplyHelper
    {
        public static List<ChargeApplyModel> GetChargeApplyModels()
        {
            var result = new List<ChargeApplyModel>();
            var Company = new ChargeApplyModel
            {
                Id = (int)ChargeApply.Company,
                Name = ChargeApply.Company.ToString(),
                Display="单位"
            };
            result.Add(Company);
            var Personal = new ChargeApplyModel
            {
                Id = (int)ChargeApply.Personal,
                Name = ChargeApply.Personal.ToString(),
                Display = "个人"
            };
            result.Add(Personal);
            var Currency = new ChargeApplyModel
            {
                Id = (int)ChargeApply.Currency,
                Name = ChargeApply.Currency.ToString(),
                Display = "通用"
            };
            result.Add(Currency);
            return result;
        }
    }
}
