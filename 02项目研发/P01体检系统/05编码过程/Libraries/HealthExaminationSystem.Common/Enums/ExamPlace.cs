using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum ExamPlace
    {
        [Description("院内")]
        Hospital = 1,
        [Description("外出")]
        GoOut = 2
    }
}
