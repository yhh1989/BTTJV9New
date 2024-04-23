using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum EditModeType
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Add,
        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete,
    }
}
