using System;
using System.Collections.Generic;
using HealthExaminationSystem.Enumerations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 查询投诉条件输入数据传输对象
    /// </summary>
    public class QueryComplaintConditionInput
    {
        /// <summary>
        /// 体检人姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 体检人手机
        /// </summary>
        public string CustomerMobile { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerRegisterCode { get; set; }

        /// <summary>
        /// 投诉时间（起）
        /// </summary>
        public DateTime? ComplainTimeStart { get; set; }

        /// <summary>
        /// 投诉时间（止）
        /// </summary>
        public DateTime? ComplainTimeEnd { get; set; }

        /// <summary>
        /// 被投诉人标识
        /// </summary>
        public long? ComplainUserId { get; set; }

        /// <summary>
        /// 处理人标识
        /// </summary>
        public long? HandlerId { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public List<ComplaintProcessState> ProcessState { get; set; }
    }
}