using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum LogsTypes
    {

        /// <summary>
        /// 单位相关
        /// </summary>
        [Description("单位相关")]
        ClientId = 1,
        /// <summary>
        /// 登记相关
        /// </summary>
        [Description("登记相关")]
        ResId = 2,
        /// <summary>
        /// 检查相关
        /// </summary>
        [Description("检查相关")]
        CheckId = 3,
        /// <summary>
        /// 收费相关
        /// </summary>
        [Description("收费相关")]
        ChargId = 4,
        /// <summary>
        /// 总检相关
        /// </summary>
        [Description("总检相关")]
        SumId = 5,
        /// <summary>
        /// 打印相关
        /// </summary>
        [Description("打印相关")]
        PrintId = 6,
        /// <summary>
        /// 接口相关
        /// </summary>
        [Description("接口相关")]
        InterId = 7,
        /// <summary>
        /// 基础资料
        /// </summary>
        [Description("基础资料")]
        BisId = 8,
        /// <summary>
        /// 其他相关
        /// </summary>
        [Description("其他相关")]
        Other = 9,
        /// <summary>
        /// 问诊相关
        /// </summary>
        [Description("问诊相关")]
        WZ = 10
    }
}
