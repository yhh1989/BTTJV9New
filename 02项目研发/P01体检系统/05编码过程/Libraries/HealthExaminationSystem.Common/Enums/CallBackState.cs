using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 回访状态
    /// </summary>
    public enum CallBackState
    {
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Complete =0,
        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Close =1,
    }
}
