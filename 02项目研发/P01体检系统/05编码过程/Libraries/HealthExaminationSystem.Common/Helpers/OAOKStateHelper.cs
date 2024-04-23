using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
  public   class OAOKStateHelper
    {

        /// <summary>
        /// 支付方式/加项方式
        /// </summary>
        /// <returns></returns>
        public static List<OAOKStateModel> GetOAOKState()
        {
            var result = new List<OAOKStateModel>();
            var NoOK = new OAOKStateModel
            {
                Id = (int)OAOKState.NoOK,
                Name = OAOKState.NoOK.ToString(),
                Display = "未确认"
            };
            result.Add(NoOK);
            var HasAp = new OAOKStateModel
            {
                Id = (int)OAOKState.HasAp,
                Name = OAOKState.HasAp.ToString(),
                Display = "已确认"
            };
            result.Add(HasAp);
          
            return result;
        }
    }
}
