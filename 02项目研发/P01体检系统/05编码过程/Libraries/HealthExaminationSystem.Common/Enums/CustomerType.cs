using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 客户类别
    /// </summary>
    public enum CustomerType
    {
        [Description("普通用户")]
        ordinary=1,
        [Description("贵宾用户")]
        Vip =2
    }
}
