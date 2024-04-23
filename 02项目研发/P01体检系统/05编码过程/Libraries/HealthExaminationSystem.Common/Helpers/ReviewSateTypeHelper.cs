using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 复查种类状态
    /// </summary>
    public static class ReviewSateTypeHelper
    {
        public static List<EnumModel> GetReviewStates()
        {
            var result = new List<EnumModel>();
            var normal = new EnumModel
            {
                Id = (int)ReviewSate.Normal,
                Name = ReviewSate.Normal.ToString(),
                Display = EnumHelper.GetEnumDesc(ReviewSate.Normal)
            };
            result.Add(normal);
            var visit = new EnumModel
            {
                Id = (int)ReviewSate.ReturnVisit,
                Name = ReviewSate.ReturnVisit.ToString(),
                Display = EnumHelper.GetEnumDesc(ReviewSate.ReturnVisit)
            };
            result.Add(visit);
            var review = new EnumModel
            {
                Id = (int)ReviewSate.Review,
                Name = ReviewSate.Review.ToString(),
                Display = EnumHelper.GetEnumDesc(ReviewSate.Review)
            };
            result.Add(review);
            return result;
        }

        public static List<EnumModel> GetAllReviewStates()
        {
            var result = new List<EnumModel>();
            var normal = new EnumModel
            {
                Id = (int)ReviewSate.Normal,
                Name = ReviewSate.Normal.ToString(),
                Display = EnumHelper.GetEnumDesc(ReviewSate.Normal)
            };
            result.Add(normal);        
            var review = new EnumModel
            {
                Id = (int)ReviewSate.Review,
                Name = ReviewSate.Review.ToString(),
                Display = EnumHelper.GetEnumDesc(ReviewSate.Review)
            };
            result.Add(review);

            var all = new EnumModel
            {
                Id = 0,
                Name = "all",
                Display = "全部"
            };
            result.Add(all);
            return result;
        }

        /// <summary>
        /// 危急值状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ReviewFormatter(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            if (Enum.IsDefined(typeof(ReviewSate), obj))
            {
                return EnumHelper.GetEnumDesc((ReviewSate)obj);
            }
            return obj.ToString();
        }
    }
}
