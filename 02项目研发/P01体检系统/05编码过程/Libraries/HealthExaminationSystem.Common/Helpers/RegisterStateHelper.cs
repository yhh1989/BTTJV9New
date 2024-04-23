using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    public class RegisterStateHelper
    {
        public static List<EnumModel> GetSelectList()
        {
            var model = new List<EnumModel>();
            var Whole = new EnumModel
            {
                Id = 0,
                Name = "Whole",
                Display = "全部"
            };
            model.Add(Whole);
            var yes = new EnumModel
            {
                Id = (int)RegisterState.Yes,
                Name = RegisterState.Yes.ToString(),
                Display = "已登记"
            };
            model.Add(yes);
            var no = new EnumModel
            {
                Id = (int)RegisterState.No,
                Name = RegisterState.No.ToString(),
                Display = "未登记"
            };
            model.Add(no);
            return model;
        }
        /// <summary>
        /// 体检类型格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ExaminationFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(RegisterState), obj))
            {
                return EnumHelper.GetEnumDesc((RegisterState)obj);
            }
            return obj.ToString();
        }
    }
}
