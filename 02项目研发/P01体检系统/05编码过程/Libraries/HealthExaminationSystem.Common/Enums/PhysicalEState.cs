using System.ComponentModel;

namespace Sw.Hospital.HealthExaminationSystem.Common.Enums
{
    /// <summary>
    /// 体检状态
    /// </summary>
    public enum PhysicalEState
    {
        /// <summary>
        /// 未体检
        /// </summary>
        [Description("未体检")]
        Not = 1,
        /// <summary>
        ///  体检中
        /// </summary>
        [Description("体检中")]
        Process = 2,
        /// <summary>
        /// 体检完成
        /// </summary>
        [Description("体检完成")]
        Complete = 3
    }
    public enum ExaminationState
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        Whole = 0,
        /// <summary>
        ///  未体检
        /// </summary>
        [Description("未体检")]
        Alr = 1,
        /// <summary>
        /// 体检中
        /// </summary>
        [Description("体检中")]
        Unchecked = 2,
        /// <summary>
        /// 体检完成
        /// </summary>
        [Description("体检完成")]
        OK = 3

    }

    //总检锁定状态枚举
    public enum SummLockedState
    {
        /// <summary>
        ///  锁定
        /// </summary>
        [Description("锁定")]
        Alr = 1,
        /// <summary>
        /// 未锁定
        /// </summary>
        [Description("未锁定")]
        Unchecked = 2

    }

    //报告领取状态枚举
    public enum ReceiveSateState
    {
        /// <summary>
        ///  未领取
        /// </summary>
        [Description("未领取")]
        Alr = 1,
        /// <summary>
        /// 已领取
        /// </summary>
        [Description("已领取")]
        Unchecked = 2

    }
    //支付方式
    public enum ClientState
    {
        /// <summary>
        ///  个人支付
        /// </summary>
        [Description("个人支付")]
        Alr = 2,
        /// <summary>
        /// 单位支付
        /// </summary>
        [Description("单位支付")]
        Unchecked = 3

    }
    //项目加减状态
    public enum MinusState
    {
        /// <summary>
        ///  正常
        /// </summary>
        [Description("正常")]
        Alr = 1,
        /// <summary>
        /// 加项
        /// </summary>
        [Description("加项")]
        Unchecked = 2,
        /// <summary>
        /// 减项
        /// </summary>
        [Description("减项")]
        ThreeUnchecked = 3

    }

}