using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    public enum InvoiceStatus
    {
        /// <summary>
        /// 收费
        /// </summary>
        [Description("收费")]
        Valid = 1,

        /// <summary>
        /// 作废
        /// </summary>
        [Description("作废")]
        Cancellation = 2,

        /// <summary>
        /// 作废
        /// </summary>
        [Description("已作废")]
        HasCanceld = 3,

        //交费标示:3.正常收费4.储值5.免费
        /// <summary>
        /// 普通
        /// </summary>
        [Description("普通")]
        NormalCharge = 4,
        /// <summary>
        /// 储值
        /// </summary>
        [Description("储值")]
        Stored = 5,      
        /// <summary>
        /// 免费
        /// </summary>
        [Description("免费")]
        FreeAdmission = 6

    }
}