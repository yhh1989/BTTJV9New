using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Market.Dto
{
    /// <summary>
    /// 合同信息数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Market.ContractInformation))]
#endif
    public class ContractInformationDto : EntityDto<Guid>
    {
        ///// <summary>
        ///// 附件集合
        ///// </summary>
        //public virtual ICollection<ContractAdjunct> AdjunctCollection { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        ///// <summary>
        ///// 类别
        ///// </summary>
        //public virtual ContractCategory Category { get; set; }

        ///// <summary>
        ///// 公司
        ///// </summary>
        //public virtual TjlClientInfo Company { get; set; }

        /// <summary>
        /// 机构标识
        /// </summary>
        public Guid CompanyId { get; set; }

        ///// <summary>
        ///// 公司预约
        ///// </summary>
        //public virtual TjlClientReg CompanyRegister { get; set; }

        /// <summary>
        /// 公司预约标识
        /// </summary>
        public Guid? CompanyRegisterId { get; set; }

        /// <summary>
        /// 合同类别标识
        /// </summary>
        public Guid ContractCategoryId { get; set; }

        /// <summary>
        /// 重要事项
        /// </summary>
        [StringLength(maximumLength: 1024)]
        public string ImportantMatter { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        [Required]
        [StringLength(maximumLength: 64)]
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Required]
        [StringLength(maximumLength: 64)]
        public string Number { get; set; }

        ///// <summary>
        ///// 回款记录集合
        ///// </summary>
        //public virtual List<ContractProceedsDto> ProceedsCollection { get; set; }

        /// <summary>
        /// 签字代表
        /// </summary>
        [StringLength(maximumLength: 64)]
        public string Signatory { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime? SubmitTime { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime? ValidTime { get; set; }
    }
}