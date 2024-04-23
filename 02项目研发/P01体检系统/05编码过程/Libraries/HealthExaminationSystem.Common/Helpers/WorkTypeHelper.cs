using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class WorkTypeHelper
    {
        public static List<WorkTypeModel> GetList()
        {
            var model = new List<WorkTypeModel>();
            var work = new WorkTypeModel()
            {
                Id = (int)WorkType.Work,
                Name = WorkType.Work.ToString(),
                Display = "工种"
            };
            var workshop = new WorkTypeModel()
            {
                Id = (int)WorkType.Workshop,
                Name = WorkType.Workshop.ToString(),
                Display = "车间"
            };
            var industry = new WorkTypeModel()
            {
                Id = (int)WorkType.Industry,
                Name = WorkType.Industry.ToString(),
                Display = "行业"
            };
            return model;
        }
    }
}
