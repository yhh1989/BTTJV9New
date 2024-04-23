using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public static class ShortMessageStateHelper
    {
        public static List<EnumModel> GetShortMessageStateStates()
        {
            var result = new List<EnumModel>();
            var open = new EnumModel
            {
                Id = (int)ShortMessageState.NoMessage,
                Name = ShortMessageState.NoMessage.ToString(),
                Display = EnumHelper.GetEnumDesc(ShortMessageState.NoMessage)
            };
            result.Add(open);
            var close = new EnumModel
            {
                Id = (int)ShortMessageState.HasMessage,
                Name = ShortMessageState.HasMessage.ToString(),
                Display = EnumHelper.GetEnumDesc(ShortMessageState.HasMessage)
            };
            result.Add(close);
            var hasBeenSent = new EnumModel
            {
                Id = (int)ShortMessageState.HasResive,
                Name = ShortMessageState.HasResive.ToString(),
                Display = EnumHelper.GetEnumDesc(ShortMessageState.HasResive)
            };
            result.Add(hasBeenSent);
            return result;
        }

        /// <summary>
        /// 体检状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ShortMessageStateFormatter(object obj)
        {
            if (obj == null)
                return string.Empty;
            if (Enum.IsDefined(typeof(ShortMessageState), obj))
            {
                return EnumHelper.GetEnumDesc((ShortMessageState)obj);
            }
            return obj.ToString();
        }
    }
}
