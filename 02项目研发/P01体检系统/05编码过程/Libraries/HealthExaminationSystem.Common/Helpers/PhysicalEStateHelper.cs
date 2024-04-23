using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class PhysicalEStateHelper
    {
        public static List<PhysicalEStateModel> GetList()
        {
            var model = new List<PhysicalEStateModel>();
            var Whole = new PhysicalEStateModel
            {
                Id = (int)ExaminationState.Whole,
                Name = ExaminationState.Whole.ToString(),
                Display = "全部"
            };
            model.Add(Whole);
            var Alr = new PhysicalEStateModel
            {
                Id = (int)ExaminationState.Alr,
                Name = ExaminationState.Alr.ToString(),
                Display = "已检"
            };
            model.Add(Alr);
            var Unchecked = new PhysicalEStateModel
            {
                Id = (int)ExaminationState.Unchecked,
                Name = ExaminationState.Unchecked.ToString(),
                Display = "未检"
            };
            model.Add(Unchecked);
            return model;
        }
        public static List<PhysicalEStateModel> YYGetList()
        {
            var model = new List<PhysicalEStateModel>();
            var Whole = new PhysicalEStateModel
            {
                Id = (int)ExaminationState.Whole,
                Name = ExaminationState.Whole.ToString(),
                Display = "全部"
            };
            model.Add(Whole);
            var Alr = new PhysicalEStateModel
            {
                Id = (int)ExaminationState.Alr,
                Name = ExaminationState.Alr.ToString(),
                Display = "未体检"
            };
            model.Add(Alr);
            var Unchecked = new PhysicalEStateModel
            {
                Id = (int)ExaminationState.Unchecked,
                Name = ExaminationState.Unchecked.ToString(),
                Display = "体检中"
            };
            model.Add(Unchecked);
            var Ok = new PhysicalEStateModel
            {
                Id = (int)ExaminationState.OK,
                Name = ExaminationState.Unchecked.ToString(),
                Display = "体检完成"
            };
            model.Add(Ok);
            return model;
        }
    }
}
