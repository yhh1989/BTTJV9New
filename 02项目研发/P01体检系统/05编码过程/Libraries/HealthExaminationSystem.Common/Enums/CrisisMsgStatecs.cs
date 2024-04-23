using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 危急值通知消息状态
    /// </summary>
    public enum CrisisMsgStatecs
    {
        /// <summary>
        /// 未发送
        /// </summary>
        [Description("未发送")]
        Not =0,
        /// <summary>
        /// 已发送
        /// </summary>
        [Description("已发送")]
        Send =1,
        /// <summary>
        /// 发送失败
        /// </summary>
        [Description("发送失败")]
        SendFail =2
    }
}
