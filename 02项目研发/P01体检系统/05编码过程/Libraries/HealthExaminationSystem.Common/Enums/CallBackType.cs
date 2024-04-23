using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 回访方式
    /// </summary>
    public enum CallBackType
    {
        [Description("微信")]
        Wechat =0,
        [Description("电话")]
        Phone =1,
        [Description("短信")]
        Message =2,
        [Description("到店")]
        Arrival =3

    }
}
