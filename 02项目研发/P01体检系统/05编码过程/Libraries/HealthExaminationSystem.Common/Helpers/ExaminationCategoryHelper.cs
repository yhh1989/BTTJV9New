using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class ExaminationCategoryHelper
    {
        public static List<EnumModel> GetExaminationCategories()
        {
            var result = new List<EnumModel>();
            var health = new EnumModel
            {
                Id = (int)ExaminationCategory.Health,
                Name = ExaminationCategory.Health.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationCategory.Health)
            };
            result.Add(health);
            var occupational = new EnumModel
            {
                Id = (int)ExaminationCategory.Occupational,
                Name = ExaminationCategory.Occupational.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationCategory.Occupational)
            };
            result.Add(occupational);

            var reExamine = new EnumModel
            {
                Id = (int)ExaminationCategory.ReExamine,
                Name = ExaminationCategory.ReExamine.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationCategory.ReExamine)
            };
            result.Add(reExamine);
            var noticeReExamine = new EnumModel
            {
                Id = (int)ExaminationCategory.NoticeReExamine,
                Name = ExaminationCategory.NoticeReExamine.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationCategory.NoticeReExamine)
            };
            result.Add(noticeReExamine);

            var irregularExamine = new EnumModel
            {
                Id = (int)ExaminationCategory.IrregularExamine,
                Name = ExaminationCategory.IrregularExamine.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationCategory.IrregularExamine)
            };
            result.Add(irregularExamine);
            var suspectedOccupationalDisease = new EnumModel
            {
                Id = (int)ExaminationCategory.SuspectedOccupationalDisease,
                Name = ExaminationCategory.SuspectedOccupationalDisease.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationCategory.SuspectedOccupationalDisease)
            };
            result.Add(suspectedOccupationalDisease);
            var occupationalContraindication = new EnumModel
            {
                Id = (int)ExaminationCategory.OccupationalContraindication,
                Name = ExaminationCategory.OccupationalContraindication.ToString(),
                Display = EnumHelper.GetEnumDesc(ExaminationCategory.OccupationalContraindication)
            };
            result.Add(occupationalContraindication);
            return result;
        }
    }
}
