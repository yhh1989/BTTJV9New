using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 结果判断状态
    /// </summary>
    public enum ResultJudgementState
    {
        /// <summary>
        /// 大小
        /// </summary>
        BigOrSmall=1,
        /// <summary>
        /// 包含文字
        /// </summary>
        ContainText=2,
        /// <summary>
        /// 等于文字
        /// </summary>
        Text=3
    }
}
