using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class CollectionStateHelper
    {
        public static List<CollectionStateModel> GetCollectionState()
        {
            var result = new List<CollectionStateModel>();
            var Normal = new CollectionStateModel
            {
                Id = (int)CollectionState.Normal,
                Name = CollectionState.Normal.ToString(),
                Display = "未核收"
            };
            result.Add(Normal);
            var Scatter = new CollectionStateModel
            {
                Id = (int)CollectionState.Scatter,
                Name = CollectionState.Scatter.ToString(),
                Display = "已核收"
            };
            result.Add(Scatter);
            return result;
        }
        /// <summary>
        /// 核收状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PayerCollectionStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(CollectionState), obj))
            {
                return EnumHelper.GetEnumDesc((CollectionState)obj);
            }
            return obj.ToString();
        }
    }
}
