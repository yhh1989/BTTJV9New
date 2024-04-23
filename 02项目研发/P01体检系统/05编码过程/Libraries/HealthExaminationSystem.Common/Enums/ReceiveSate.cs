using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum ReceiveSate
    {
        [Description("未领取")]
        UnReceived = 1,
        [Description("已领取")]
        AreReceived = 2
    }
}
