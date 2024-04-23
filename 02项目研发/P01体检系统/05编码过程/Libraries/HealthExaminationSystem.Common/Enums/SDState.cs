using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum SDState
    {
        [Description("锁定")]
        Locking = 1,
        [Description("未锁定")]
        Unlocked = 2
    }
}
