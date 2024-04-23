using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 导出状态
    /// </summary>
    public enum ExportSate
    {
        [Description("未导出")]
        NotToExport = 1,

        [Description("已导出")]
        Export = 2
    }
}
