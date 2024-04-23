using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto
{
    /// <summary>
    /// 体检人预约条码打印信息条件输入数据传输对象
    /// </summary>
    public class CustomerRegisterBarCodePrintInformationConditionInput
    {
        /// <summary>
        /// 条码号
        /// </summary>
        public string BarCodeNumber { get; set; }

        /// <summary>
        /// 抽血时间
        /// </summary>
        public DateTime? BloodTime { get; set; }
        /// <summary>
        /// 抽血开始时间
        /// </summary>
        public DateTime? StarBloodTime { get; set; }
        /// <summary>
        /// 抽血结束时间
        /// </summary>
        public DateTime? EndBloodTime { get; set; }
        /// <summary>
        /// 抽血人标识
        /// </summary>
        public long? BloodUserId { get; set; }

        /// <summary>
        /// 抽血状态
        /// </summary>
        public bool? HaveBlood { get; set; }

        /// <summary>
        /// 自动抽血
        /// </summary>
        public bool AutoBlood { get; set; }
        /// <summary>
        /// 自动送检
        /// </summary>
        public bool AutoSend { get; set; }
        /// <summary>
        /// 自动接收
        /// </summary>
        public bool AutoReceive { get; set; }
    }
}