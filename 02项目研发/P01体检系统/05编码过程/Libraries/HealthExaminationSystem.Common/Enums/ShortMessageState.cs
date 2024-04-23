using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
  public enum ShortMessageState
    {
        /// <summary>
        /// 未发送
        /// </summary>
        [Description("未发送")]
        NoMessage = 0,

        /// <summary>
        /// 已发送
        /// </summary>
        [Description("已发送")]
        HasMessage = 1,

        /// <summary>
        /// 已接收
        /// </summary>
        [Description("已接收")]
        HasResive = 2,
        /// <summary>
        /// 发送失败
        /// </summary>
        [Description("发送失败")]
        SendErr = 3,
        /// <summary>
        /// 接收失败
        /// </summary>
        [Description("接收失败")]
        ResiveErr = 4,
    }
}
