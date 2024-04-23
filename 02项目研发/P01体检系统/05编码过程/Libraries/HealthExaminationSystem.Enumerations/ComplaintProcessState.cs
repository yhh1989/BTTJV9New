using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthExaminationSystem.Enumerations
{
    /// <summary>
    /// 投诉处理状态
    /// </summary>
    public enum ComplaintProcessState
    {
        /// <summary>
        /// 未处理
        /// </summary>
        Undisposed,
        /// <summary>
        /// 已处理
        /// </summary>
        Processed,
        /// <summary>
        /// 忽略
        /// </summary>
        Ignored,
        /// <summary>
        /// 已上报
        /// </summary>
        Reported
    }
}
