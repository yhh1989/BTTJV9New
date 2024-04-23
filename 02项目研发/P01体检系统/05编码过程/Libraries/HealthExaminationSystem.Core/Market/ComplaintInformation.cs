using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using HealthExaminationSystem.Enumerations;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Market
{
    /// <summary>
    /// 投诉信息
    /// </summary>
    [Table("ComplaintInformation")]
    public class ComplaintInformation : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人标识
        /// </summary>
        [ForeignKey(nameof(Customer))]
        [Column("CustomerId")]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual TjlCustomer Customer { get; set; }

        /// <summary>
        /// 单位信息标识
        /// </summary>
        [Column("CompanyInformationId")]
        [ForeignKey(nameof(CompanyInformation))]
        public Guid? CompanyInformationId { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        public virtual TjlClientInfo CompanyInformation { get; set; }

        /// <summary>
        /// 单位预约信息标识
        /// </summary>
        [Column("CompanyRegisterId")]
        [ForeignKey(nameof(CompanyRegister))]
        public Guid? CompanyRegisterId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual TjlClientReg CompanyRegister { get; set; }

        /// <summary>
        /// 单位预约分组信息标识
        /// </summary>
        [Column("CompanyRegisterTeamId")]
        [ForeignKey(nameof(CompanyRegisterTeamInformation))]
        public Guid? CompanyRegisterTeamId { get; set; }

        /// <summary>
        /// 单位预约分组信息
        /// </summary>
        public virtual TjlClientTeamInfo CompanyRegisterTeamInformation { get; set; }

        /// <summary>
        /// 体检人预约标识
        /// </summary>
        [ForeignKey(nameof(CustomerRegister))]
        [Column("CustomerRegisterId")]
        public Guid CustomerRegisterId { get; set; }

        /// <summary>
        /// 体检人预约信息
        /// </summary>
        public virtual TjlCustomerReg CustomerRegister { get; set; }

        /// <summary>
        /// 投诉内容
        /// </summary>
        [Column("Description")]
        [MaxLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [Column("Result")]
        [MaxLength(512)]
        public string Result { get; set; }

        /// <summary>
        /// 投诉方式
        /// </summary>
        /// <remarks>
        /// 请参考：<see cref="TbmBasicDictionary"/>
        /// <para>
        /// 类型：Sw.Hospital.HealthExaminationSystem.Common.Enums.BasicDictionaryType.ComplainWay
        /// </para>
        /// </remarks>
        [Column("ComplainWay")]
        public int? ComplainWay { get; set; }

        /// <summary>
        /// 投诉类别
        /// </summary>
        /// <remarks>
        /// 请参考：<see cref="TbmBasicDictionary"/>
        /// <para>
        /// 类型：Sw.Hospital.HealthExaminationSystem.Common.Enums.BasicDictionaryType.ComplainCategory
        /// </para>
        /// </remarks>
        [Column("ComplainCategory")]
        public int? ComplainCategory { get; set; }

        /// <summary>
        /// 投诉时间
        /// </summary>
        [Column("ComplainTime")]
        public DateTime ComplainTime { get; set; }

        /// <summary>
        /// 被投诉人标识
        /// </summary>
        [ForeignKey(nameof(ComplainUser))]
        [Column("ComplainUserId")]
        public long? ComplainUserId { get; set; }

        /// <summary>
        /// 被投诉人
        /// </summary>
        public virtual User ComplainUser { get; set; }

        /// <summary>
        /// 处理人标识
        /// </summary>
        [ForeignKey(nameof(Handler))]
        [Column("HandlerId")]
        public long HandlerId { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public User Handler { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        [Column("ProcessState")]
        public ComplaintProcessState ProcessState { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [Column("ProcessingTime")]
        public DateTime? ProcessingTime { get; set; }

        /// <summary>
        /// 紧急级别
        /// </summary>
        [Column("ExigencyLevel")]
        public ComplaintExigencyLevel ExigencyLevel { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}