using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum ReportState
    {
        [Description("所有状态")]
        All = 0,
        [Description("可打印")]
        AllowPrint = 1,
        [Description("未完成")]
        NotCompleted = 2
    }
}
