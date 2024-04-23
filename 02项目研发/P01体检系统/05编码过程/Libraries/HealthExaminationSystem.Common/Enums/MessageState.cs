using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{ 
    /// <summary>
    /// 通知状态
    /// </summary>
    public enum  MessageState
    {
        /// <summary>
        /// 未通知
        /// </summary>
        [Description("未通知")]
        NoMessage = 1,

        /// <summary>
        /// 已通知
        /// </summary>
        [Description("已通知")]
        HasMessage = 2
    }
}
