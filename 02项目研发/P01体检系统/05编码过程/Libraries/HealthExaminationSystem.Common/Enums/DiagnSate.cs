using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 是否符合判断
    /// </summary>
    public enum DiagnSate
    {
        /// <summary>
        /// 复合判断
        /// </summary>
        [Description("复合判断")]
        Judge = 1,
        /// <summary>
        /// 非复合判断
        /// </summary>
        [Description("非复合判断")]
        WrongJudge = 2
    }
}
