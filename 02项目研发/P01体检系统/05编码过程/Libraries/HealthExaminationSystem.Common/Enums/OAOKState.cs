using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum OAOKState
    {
        /// <summary> 
        ///未确认 
        /// </summary>
        [Description("未审批")]
        NoOK = 0,
        /// <summary>
        /// 已确认
        /// </summary>
        [Description("已确认")]
        HasAp = 1
      
    }
}
