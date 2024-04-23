using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    ///  体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
    /// </summary>
    public enum PhysicalType
    {
        [Description("健康体检")]
        Health = 1,
        [Description("职业健康体检")]
        Occupational = 2,
        [Description("健康证体检")]
        HealthCertificate = 3,
        [Description("公务员体检")]
        Civil = 4,
        [Description("学生体检")]
        Student = 5,
        [Description("驾驶证体检")]
        Driver = 6,
        [Description("婚检")]
        Marriage = 7

    }
}
