using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 回访状态0未回访1已回访3已取消
    /// </summary>
    public enum VisiteState
    {
        /// <summary>
        /// 未回访
        /// </summary>
        [Description("未回访")]
        NoVisite = 0,

        /// <summary>
        /// 已回访
        /// </summary>
        [Description("已回访")]
        HasVisite = 1,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        CancelVisite = 2

    }
}
