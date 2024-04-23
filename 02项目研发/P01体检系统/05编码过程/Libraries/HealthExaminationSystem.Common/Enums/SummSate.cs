using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 总检状态 1未总检2已初审3已初检4已审核
    /// </summary>
    public enum SummSate
    {
        /// <summary>
        /// 未总检
        /// </summary>
        [Description("未总检")]
        NotAlwaysCheck = 1,

        /// <summary>
        /// 已初审
        /// </summary>
        [Description("可总检")]
        HasBeenEvaluated = 2,

        /// <summary>
        /// 已初审
        /// </summary>
        [Description("已初审")]
        HasInitialReview = 3,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        Audited = 4,
        /// <summary>
        /// 审核未通过
        /// </summary>
        [Description("审核未通过")]
        AuditFailed = 5
    }
}
