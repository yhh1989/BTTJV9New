using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum ClientCheckSate
    {
        [Description("完成")]
        Complete = 1,
        [Description("未完成")]
        NotComplete = 2
    }
}
