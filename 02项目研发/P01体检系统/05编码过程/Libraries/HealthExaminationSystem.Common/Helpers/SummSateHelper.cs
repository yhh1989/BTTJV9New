using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class SummSateHelper
    {
        private static List<EnumModel> _summStateModels = new List<EnumModel>();

        static SummSateHelper()
        {
            var whole = new EnumModel
            {
                Id = 0,
                Name = "Whole",
                Display = "全部"
            };
            _summStateModels.Add(whole);
            var notAlwaysCheck = new EnumModel
            {
                Id = (int)SummSate.NotAlwaysCheck,
                Name = SummSate.NotAlwaysCheck.ToString(),
                Display = "未总检"
            };
            _summStateModels.Add(notAlwaysCheck);

            var hasBeenEvaluated = new EnumModel
            {
                Id = (int)SummSate.HasBeenEvaluated,
                Name = SummSate.HasBeenEvaluated.ToString(),
                Display = "可总检"
            };
            _summStateModels.Add(hasBeenEvaluated);
            var hasInitialReview = new EnumModel
            {
                Id = (int)SummSate.HasInitialReview,
                Name = SummSate.HasInitialReview.ToString(),
                Display = "已初审"
            };
            _summStateModels.Add(hasInitialReview);
            var audited = new EnumModel
            {
                Id = (int)SummSate.Audited,
                Name = SummSate.Audited.ToString(),
                Display = "已审核"
            };
            _summStateModels.Add(audited);
        }

        public static List<EnumModel> GetSelectList()
        {
            return _summStateModels;
        }
        /// <summary>
        /// 体检状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SummSateFormatter(object obj)
        {
            if (obj == null)
                return string.Empty;
            if (Enum.IsDefined(typeof(SummSate), obj))
            {
                return EnumHelper.GetEnumDesc((SummSate)obj);
            }
            return obj.ToString();
        }
    }
}
