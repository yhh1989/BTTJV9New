using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 确认状态
    /// </summary>
    public enum ConfirmState
    {
        [Description("未确认")]
        Unconfirmed = 1,
        [Description("已确认")]
        Confirmed = 2
    }
}
