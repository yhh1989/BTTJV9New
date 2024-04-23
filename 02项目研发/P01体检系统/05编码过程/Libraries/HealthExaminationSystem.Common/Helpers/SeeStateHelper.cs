using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class SeeStateHelper
    {
        public static List<SeeStateModel> GetSeeStateModels()
        {
            var result = new List<SeeStateModel>();
            var See = new SeeStateModel {
                Id = (int)SeeState.See,
                Name=SeeState.See.ToString(),
                Display="所见"
            };
            result.Add(See);
            var Diagnosis = new SeeStateModel
            {
                Id = (int)SeeState.Diagnosis,
                Name = SeeState.Diagnosis.ToString(),
                Display = "诊断"
            };
            result.Add(Diagnosis);
            var Normal = new SeeStateModel
            {
                Id = (int)SeeState.Normal,
                Name = SeeState.Normal.ToString(),
                Display = "正常"
            };
            result.Add(Normal);
            return result;
        }
    }
}
