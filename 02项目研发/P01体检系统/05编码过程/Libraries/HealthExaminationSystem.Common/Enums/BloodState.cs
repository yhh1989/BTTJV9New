using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum BloodState
    {
        [Description("已抽血")]
        DrawBlood =1,
        [Description("未抽血")]
        NOT = 2,
        [Description("无须抽血")]
        NOTNEED = 3,
        [Description("已取消")]
        Cancel = 4,
    }
}
