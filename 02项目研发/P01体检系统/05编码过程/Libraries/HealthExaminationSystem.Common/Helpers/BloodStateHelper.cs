using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class BloodStateHelper
    {
        public static List<BloodStateModel> GetBloodStateModel()
        {
            var result = new List<BloodStateModel>();
            var DrawBlood = new BloodStateModel {
                Id = (int)BloodState.DrawBlood,
                Name = BloodState.DrawBlood.ToString(),
                Display="已抽血"
            };
            result.Add(DrawBlood);
            var Cancel = new BloodStateModel
            {
                Id = (int)BloodState.Cancel,
                Name = BloodState.Cancel.ToString(),
                Display = "已取消"
            };
            result.Add(Cancel);
            var NOT = new BloodStateModel
            {
                Id = (int)BloodState.Cancel,
                Name = BloodState.NOT.ToString(),
                Display = "未抽血"
            };
            result.Add(NOT);
            var NOTNEED = new BloodStateModel
            {
                Id = (int)BloodState.NOTNEED,
                Name = BloodState.Cancel.ToString(),
                Display = "无须抽血"
            };
            result.Add(NOTNEED);
            return result;
        }
    }
}
