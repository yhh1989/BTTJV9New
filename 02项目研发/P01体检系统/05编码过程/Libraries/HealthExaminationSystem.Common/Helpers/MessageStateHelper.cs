using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
  public static class MessageStateHelper
    {

        /// <summary>
        /// 所有状态
        /// </summary>
        /// <returns></returns>
        public static List<MessageStateModel> GetList()
        {
            var model = new List<MessageStateModel>();
            var All = new MessageStateModel
            {
                Id =0,
                Name ="All",
                Display = "全部"
            };
            model.Add(All);
            var NoMess = new MessageStateModel
            {
                Id = (int)MessageState.NoMessage,
                Name = MessageState.NoMessage.ToString(),
                Display = "未通知"
            };
            model.Add(NoMess);
            var HsMess = new MessageStateModel
            {
                Id = (int)MessageState.HasMessage,
                Name = MessageState.HasMessage.ToString(),
                Display = "已通知"
            };
            model.Add(HsMess);
            return model;
        }
       
        /// <summary>
        /// 通知状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CriticalTypeStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(MessageState), obj))
            {
                return EnumHelper.GetEnumDesc((MessageState)obj);
            }
            return obj.ToString();
        }
    }
}
