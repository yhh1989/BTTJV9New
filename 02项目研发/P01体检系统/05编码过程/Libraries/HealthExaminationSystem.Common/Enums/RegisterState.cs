using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum RegisterState
    {
        /// <summary>
        /// 未登记
        /// </summary>
        [Description("未登记")]
        No =1,
        /// <summary>
        /// 已登记
        /// </summary>
        [Description("已登记")]
        Yes =2
    }
}
