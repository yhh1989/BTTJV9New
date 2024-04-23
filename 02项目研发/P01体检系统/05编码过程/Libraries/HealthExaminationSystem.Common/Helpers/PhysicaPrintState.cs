using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Helpers
{
   public  enum PhysicaPrintState
    {
        /// <summary>
        /// 已打印
        /// </summary>
        [Description("已打印")]
        print = 1,
        /// <summary>
        /// 未打印
        /// </summary>
        [Description("未打印")]
        Notprint = 2,
        /// <summary>
        /// 已打印
        /// </summary>
        [Description("无需打印")]
        Disprint = 3,


    }
}
