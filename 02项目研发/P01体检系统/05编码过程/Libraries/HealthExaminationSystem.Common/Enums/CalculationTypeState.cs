using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum CalculationTypeState
    {
        /// <summary>
        /// 数值型
        /// </summary>
        [Description("数值型")]
        Numerical = 1,
        /// <summary>
        /// 文本型
        /// </summary>
        [Description("文本型")]
        Text = 2,
    }
}
