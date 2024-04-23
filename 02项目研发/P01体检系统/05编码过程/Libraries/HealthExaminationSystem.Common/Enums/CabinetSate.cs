using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
  public  enum CabinetSate
    {
        /// <summary>
        /// 数字字符
        /// </summary>
        [Description("数字字符")]
        Numfomat = 0,
        /// <summary>
        /// 英文字符
        /// </summary>
        [Description("英文字符")]
        YWfomat = 1,
    }
}
