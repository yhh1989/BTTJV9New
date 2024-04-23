using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum  IllnessLevel
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        Nothing = 0,
        /// <summary>
        /// 轻微
        /// </summary>
        [Description("轻微")]
        Light = 1,

        /// <summary>
        /// 中度
        /// </summary>
        [Description("中度")]
         Middle = 2,

        /// <summary>
        /// 重度
        /// </summary>
        [Description("重度")]
        Severe = 3,
    }
}
