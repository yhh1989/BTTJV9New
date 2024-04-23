using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum ClientSate
    {
        [Description("正常")]
        Normal=1,
        [Description("散检单位")]
        Scatter =2
    }
}
