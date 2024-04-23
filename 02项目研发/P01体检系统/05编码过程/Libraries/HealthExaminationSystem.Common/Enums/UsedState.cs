using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum UsedState
    {
        [Description("未使用")]
        NoUser = 0,
        [Description("已使用")]
        Userd = 1,
        [Description("全部")]
        All = 2,
    }
}
