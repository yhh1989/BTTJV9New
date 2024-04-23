using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum OAApState
    {
        /// <summary> 
        ///未审批 
        /// </summary>
        [Description("未审批")]
        NoAp = 1,
        /// <summary>
        /// 已审批
        /// </summary>
        [Description("已审批")]
        HasAp = 2,
        /// <summary>
        /// 已拒绝
        /// </summary>
        [Description(" 已拒绝")]
        reAp = 3,
    }
}
