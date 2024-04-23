using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
   public  enum ClientZKState
    {
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        Normal = 1,
        /// <summary>
        /// 有权限
        /// </summary>
        [Description("有权限")]
        Scatter = 2
    }
}
