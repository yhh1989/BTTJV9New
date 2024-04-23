using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    ///  危急值回访状态 0未上报1已上报2已取消3已审核
    /// </summary>
    public enum CrisisVisitSate
    {
        /// <summary>
        /// 未上报
        /// </summary>
        [Description("未上报")]
        No = 0,
        /// <summary>
        /// 已上报
        /// </summary>
        [Description("已上报")]
        Yes = 1,
        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Concel = 2,
        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        Examine = 3,
    }
}
