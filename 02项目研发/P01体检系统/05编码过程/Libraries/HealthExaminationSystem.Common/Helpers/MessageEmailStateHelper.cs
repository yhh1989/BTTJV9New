using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 人员登记 是否启用短信/邮件 1启用2不启用3已发生
    /// </summary
    public static class MessageEmailStateHelper
    {
        public static List<EnumModel> GetMessageEmailStates()
        {
            var result = new List<EnumModel>();
            var open = new EnumModel
            {
                Id = (int)MessageEmailState.Open,
                Name = MessageEmailState.Open.ToString(),
                Display = EnumHelper.GetEnumDesc(MessageEmailState.Open)
            };
            result.Add(open);
            var close = new EnumModel
            {
                Id = (int)MessageEmailState.Close,
                Name = MessageEmailState.Close.ToString(),
                Display = EnumHelper.GetEnumDesc(MessageEmailState.Close)
            };
            result.Add(close);
            var hasBeenSent = new EnumModel
            {
                Id = (int)MessageEmailState.HasBeenSent,
                Name = MessageEmailState.HasBeenSent.ToString(),
                Display = EnumHelper.GetEnumDesc(MessageEmailState.HasBeenSent)
            };
            result.Add(hasBeenSent);
            return result;
        }
    }
}
