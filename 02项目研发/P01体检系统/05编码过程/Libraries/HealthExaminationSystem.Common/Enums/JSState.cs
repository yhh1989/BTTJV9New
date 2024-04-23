using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
   public enum JSState
    {
        [Description("未核算")]
        not = 0,      
        [Description("已核算")]
        Already = 1
    }
}
