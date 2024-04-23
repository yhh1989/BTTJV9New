using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 项目套餐类型
    /// </summary>
    public enum ItemSuitType
    {
        /// <summary>
        /// 套餐
        /// </summary>
        [Description("套餐")]
        Suit = 1,
        ///// <summary>
        ///// 组单
        ///// </summary>
        //[Obsolete("停止使用", true)]
        //[Description("组单")]
        //Building = 2,
        /// <summary>
        /// 1+X
        /// </summary>
        [Description("1+X")]
        OnePlusX = 3,
        [Description("线上套餐")]
        OnLine = 4,
    }
}
