using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum CollectionState
    {
        /// <summary>
        /// 未核收
        /// </summary>
        [Description("未核收")]
        Normal = 1,
        /// <summary>
        /// 已核收
        /// </summary>
        [Description("已核收")]
        Scatter = 2
    }
}
