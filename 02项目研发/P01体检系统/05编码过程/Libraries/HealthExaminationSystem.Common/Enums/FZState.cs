using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum FZState
    {
        [Description("已封账")]
        Already = 1,
        [Description("未封账")]
        not = 2
    }
}
