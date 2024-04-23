using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum ExaminationStatus
    {
        /// <summary>
        /// 未登记
        /// </summary>
        [Description("未登记")]
        Unregistered = 1,
        /// <summary>
        ///  已登记
        /// </summary>
        [Description("已登记")]
        Registered = 2,
        /// <summary>
        /// 体检中
        /// </summary>
        [Description("体检中")]
        Examining = 3,
        /// <summary>
        /// 体检完成
        /// </summary>
        [Description("体检完成")]
        ExaminationCompleted = 4,
        /// <summary>
        ///  总检完成
        /// </summary>
        [Description("总检完成")]
        TotalExaminationCompleted = 5,
        /// <summary>
        /// 总检审核完成
        /// </summary>
        [Description("总检审核完成")]
        TotalExaminationCheckedCompleted = 6,
        /// <summary>
        /// 报告输出
        /// </summary>
        [Description("报告输出")]
        ReportOupput = 7,
        /// <summary>
        ///  报告打印
        /// </summary>
        [Description("报告打印")]
        ReportPrint = 8,
        /// <summary>
        /// 报告导出
        /// </summary>
        [Description("报告导出")]
        ReportExport = 9
    }
}
