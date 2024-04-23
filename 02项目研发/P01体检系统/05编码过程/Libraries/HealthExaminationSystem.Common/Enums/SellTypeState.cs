using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum SellTypeState
    {
        [Description("先返款")]
        Begin = 1,
        [Description("后返款")]
        After = 2,
        [Description("全部")]
        All = 3,
    }
}
