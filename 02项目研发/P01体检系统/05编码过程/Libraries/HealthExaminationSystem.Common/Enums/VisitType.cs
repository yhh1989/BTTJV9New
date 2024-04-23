using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 回访方式1短信2电话
    /// </summary>
    public enum VisitType
    {
        /// <summary>
        /// 短信
        /// </summary>
        [Description("短信")]
        Short = 1,

        /// <summary>
        /// 电话
        /// </summary>
        [Description("电话")]
        Tel = 2,
    }
}
