using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
    /// <summary>
    /// 项目类型帮助
    /// </summary>
    public class ItemTypeHelper
    {
        /// <summary>
        /// 获取项目类型
        /// </summary>
        /// <returns></returns>
        public static List<ItemTypeModel> GetItemTypeModels()
        {
            var result = new List<ItemTypeModel>();
            var calculation = new ItemTypeModel
            {
                Id = (int)ItemType.Calculation,
                Name = ItemType.Calculation.ToString(),
                Display = "计算型"
            };
            result.Add(calculation);
            var explain = new ItemTypeModel
            {
                Id = (int)ItemType.Explain,
                Name = ItemType.Explain.ToString(),
                Display = "数据文字混合型"
            };
            result.Add(explain);
            var number = new ItemTypeModel
            {
                Id = (int)ItemType.Number,
                Name = ItemType.Number.ToString(),
                Display = "数值型"
            };
            result.Add(number);
            var yinYang = new ItemTypeModel
            {
                Id = (int)ItemType.YinYang,
                Name = ItemType.YinYang.ToString(),
                Display = "阴阳型"
            };
            result.Add(yinYang);
            return result;
        }

        /// <summary>
        /// 项目格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ItemTypeFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(ItemType), obj))
            {
                return EnumHelper.GetEnumDesc((ItemType)obj);
            }
            return obj.ToString();
        }
    }
}
