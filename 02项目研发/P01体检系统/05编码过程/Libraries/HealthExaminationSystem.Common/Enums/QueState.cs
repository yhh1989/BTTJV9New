using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum QueState
    {
        /// <summary>
        /// 未扫描
        /// </summary>
        [Description("未扫描")]
        NOSM = 0,
        /// <summary>
        /// 已扫描
        /// </summary>
        [Description("已扫描")]
        YSM = 1
    }
}
