using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 人员登记 是否启用短信/邮件 1启用2不启用3已发生
    /// </summary>
    public enum MessageEmailState
    {
        /// <summary>
        /// 开启
        /// </summary>
        [Description("开启")]
        Open =1,
        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Close =2,
        /// <summary>
        /// 已发送
        /// </summary>
        [Description("已发送")]
        HasBeenSent =3,

    }
}
