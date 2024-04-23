using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
  public   class OAApStateHelper
    {

        /// <summary>
        /// 团体收费审核状态
        /// </summary>
        /// <returns></returns>
        public static List<OAApStateModel> getOAApState()
        {
            var result = new List<OAApStateModel>();
            var NoAp = new OAApStateModel
            {
                Id = (int)OAApState.NoAp,
                Name = OAApState.NoAp.ToString(),
                Display = "未审批"
            };
            result.Add(NoAp);
            var HasAp = new OAApStateModel
            {
                Id = (int)OAApState.HasAp,
                Name = OAApState.HasAp.ToString(),
                Display = "已审批"
            };
            result.Add(HasAp);
            var reAp = new OAApStateModel
            {
                Id = (int)OAApState.reAp,
                Name = OAApState.reAp.ToString(),
                Display = "已拒绝"
            };
            result.Add(reAp);

            return result;
        }
    }
}
