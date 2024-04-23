using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum Symbol
    {
        /// <summary>
        /// 偏高
        /// </summary>
        [Description("H")]
        High = 0,
        /// <summary>
        /// 超高
        /// </summary>
        [Description("HH")]
        Superhigh = 1,
        /// <summary>
        /// 偏低 
        /// </summary>
        [Description("L")]
        Low = 2,
        /// <summary>
        /// 超低
        /// </summary>
        [Description("LL")]
        UltraLow = 3,
        /// <summary>
        /// 正常
        /// </summary>
        [Description("M")]
        Normal = 4,
        /// <summary>
        /// 异常
        /// </summary>
        [Description("P")]
        Abnormal = 5,
    }
}
