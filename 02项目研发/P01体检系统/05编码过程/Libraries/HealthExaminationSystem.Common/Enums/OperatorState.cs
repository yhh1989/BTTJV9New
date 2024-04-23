using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 运算符
    /// </summary>
 public enum OperatorState
    {
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description(">=大于等于")]
        BigEqual = 1,
        /// <summary>
        /// 大于
        /// </summary>
        [Description(">大于")]
        Big = 2,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("<=小于等于")]
        SmallEqual = 3,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("<小于")]
        Small  = 4,
        /// <summary>
        /// 等于
        /// </summary>
        [Description("=等于")]
        Equal = 5,
    }
}
