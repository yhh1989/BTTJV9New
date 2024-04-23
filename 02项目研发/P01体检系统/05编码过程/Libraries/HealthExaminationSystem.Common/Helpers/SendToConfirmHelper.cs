using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 交表状态帮助
    /// </summary>
    public class SendToConfirmHelper
    {
        /// <summary>
        /// 获取交表状态
        /// </summary>
        /// <returns></returns>
        public static List<SendToConfirmModel> GetSendToConfirmModels()
        {
            var result = new List<SendToConfirmModel>();
            var No = new SendToConfirmModel
            {
                Id = (int)SendToConfirm.No,
                Name = SendToConfirm.No.ToString(),
                Display = "未交表"
            };
            result.Add(No);
            var Yes = new SendToConfirmModel
            {
                Id = (int)SendToConfirm.Yes,
                Name = SendToConfirm.Yes.ToString(),
                Display = "已交表"
            };
            result.Add(Yes);
            return result;
        }
        /// <summary>
        /// 交表状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SendToConfirmFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(SendToConfirm), obj))
            {
                return EnumHelper.GetEnumDesc((SendToConfirm)obj);
            }
            return obj.ToString();

        }

    }
}
