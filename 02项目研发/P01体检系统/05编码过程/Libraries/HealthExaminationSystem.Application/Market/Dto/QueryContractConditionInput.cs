using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 查询合同条件输入数据传输对象
    /// </summary>
    public class QueryContractConditionInput
    {
        /// <summary>
        /// 编号
        /// </summary>
        [StringLength(maximumLength: 64)]
        public string Number { get; set; }

        /// <summary>
        /// 机构标识
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// 签字代表
        /// </summary>
        [StringLength(maximumLength: 64)]
        public string Signatory { get; set; }

        /// <summary>
        /// 提交时间(起)
        /// </summary>
        public DateTime? SubmitTimeStart { get; set; }

        /// <summary>
        /// 提交时间(止)
        /// </summary>
        public DateTime? SubmitTimeEnd { get; set; }

        /// <summary>
        /// 有效时间(起)
        /// </summary>
        public DateTime? ValidTimeStart { get; set; }

        /// <summary>
        /// 有效时间(止)
        /// </summary>
        public DateTime? ValidTimeEnd { get; set; }

        /// <summary>
        /// 创建用户标识
        /// </summary>
        public long? CreatorUserId { get; set; }
    }
}