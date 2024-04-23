using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 是否进入小结
    /// </summary>
    public enum Summary
    {
        /// <summary>
        /// 进入
        /// </summary>
        [Description("进入")]
        GetInto = 1,
        /// <summary>
        /// 不进入
        /// </summary>
        [Description("不进入")]
        NotToEnter = 2
    }
}
