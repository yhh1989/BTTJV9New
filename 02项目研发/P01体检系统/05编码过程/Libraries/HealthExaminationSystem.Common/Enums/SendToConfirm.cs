using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum SendToConfirm
    {
        /// <summary>
        /// 未交表
        /// </summary>
        [Description("未交表")]
        No = 1,
        /// <summary>
        /// 已交表
        /// </summary>
        [Description("已交表")]
        Yes = 2
    }
}
