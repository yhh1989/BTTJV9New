using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
  public   class CrisisVisitSateHelper
    {
        /// <summary>
        /// 所有状态
        /// </summary>
        /// <returns></returns>
        public static List<CrisisVisitSateModel> GetList()
        {
            var model = new List<CrisisVisitSateModel>();
            var Al = new CrisisVisitSateModel
            {
                Id = (int)CrisisVisitSate.No,
                Name = CrisisVisitSate.No.ToString(),
                Display = "未上报"
            };
            model.Add(Al);
            var Bl = new CrisisVisitSateModel
            {
                Id = (int)CrisisVisitSate.Yes,
                Name = CrisisVisitSate.Yes.ToString(),
                Display = "已上报"
            };
            model.Add(Bl);
            var CL = new CrisisVisitSateModel
            {
                Id = (int)CrisisVisitSate.Concel,
                Name = CrisisVisitSate.Concel.ToString(),
                Display = "已取消"
            };
            model.Add(CL);
            var DL = new CrisisVisitSateModel
            {
                Id = (int)CrisisVisitSate.Examine,
                Name = CrisisVisitSate.Examine.ToString(),
                Display = "已审核"
            };
            model.Add(DL);
            return model;
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        /// <returns></returns>
        public static List<CrisisVisitSateModel> GetSHList()
        {
            var model = new List<CrisisVisitSateModel>();
         
                
         
            var DL = new CrisisVisitSateModel
            {
                Id = (int)CrisisVisitSate.Examine,
                Name = CrisisVisitSate.Examine.ToString(),
                Display = "已审核"
            };
            model.Add(DL);
            var Al = new CrisisVisitSateModel
            {
                Id = 4,
                Name = CrisisVisitSate.No.ToString(),
                Display = "未审核"
            };
            model.Add(Al);
            var CL = new CrisisVisitSateModel
            {
                Id = (int)CrisisVisitSate.Concel,
                Name = CrisisVisitSate.Concel.ToString(),
                Display = "已取消"
            };
            model.Add(CL);
            return model;
        }
        /// <summary>
        /// 危急值状态格式化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string CriticalTypeStateFormatter(object obj)
        {
            if (Enum.IsDefined(typeof(CrisisVisitSate), obj))
            {
                return EnumHelper.GetEnumDesc((CrisisVisitSate)obj);
            }
            return obj.ToString();
        }
    }
}
