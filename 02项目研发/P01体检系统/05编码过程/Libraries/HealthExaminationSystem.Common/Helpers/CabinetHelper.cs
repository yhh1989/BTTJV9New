using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
   public  class CabinetHelper
    {
        // <summary>
        /// 获取系统是否类列表
        /// </summary>
        /// <returns></returns>
        public static List<CabinetModel> GetIfTypeModels()
        {
            var result = new List<CabinetModel>();
            var Abnormal = new CabinetModel
            {
                Id = (int)CabinetSate.Numfomat,
                Name = CabinetSate.Numfomat.ToString(),
                Display = "数字字符"
            };
            result.Add(Abnormal);
            var Auxiliary = new CabinetModel
            {
                Id = (int)CabinetSate.YWfomat,
                Name = CabinetSate.YWfomat.ToString(),
                Display = "英文字符"
            };
            result.Add(Auxiliary);
            
            return result;
        }

        // <summary>
        /// 获取系统是否类列表
        /// </summary>
        /// <returns></returns>
        public static List<CabinetModel> GetCRModels()
        {
            var result = new List<CabinetModel>();
            var Abnormal = new CabinetModel
            {
                Id = 0,
                Name ="wcr",
                Display = "未存入"
            };
            result.Add(Abnormal);
            var Auxiliary = new CabinetModel
            {
                Id = 1,
                Name = "cr",
                Display = "存入"
            };
            result.Add(Auxiliary);
            var Allxiliary = new CabinetModel
            {
                Id = 2,
                Name = "qb",
                Display = "全部"
            };
            result.Add(Allxiliary);

            return result;
        }

        // <summary>
        /// 获取系统是否类列表
        /// </summary>
        /// <returns></returns>
        public static List<CabinetModel> GetLQModels()
        {
            var result = new List<CabinetModel>();
            var Abnormal = new CabinetModel
            {
                Id = 1,
                Name = "wcr",
                Display = "未领取"
            };
            result.Add(Abnormal);
            var Auxiliary = new CabinetModel
            {
                Id = 2,
                Name = "cr",
                Display = "已领取"
            };
            result.Add(Auxiliary);
            var Allxiliary = new CabinetModel
            {
                Id = 3,
                Name = "qb",
                Display = "全部"
            };
            result.Add(Allxiliary);

            return result;
        }
        /// <summary>
        /// 疾病状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string PositiveStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(CabinetSate), obj))
            {
                return EnumHelper.GetEnumDesc((CabinetSate)obj);
            }
            return obj.ToString();
        }
    }
}
