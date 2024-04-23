using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;

namespace Sw.Hospital.HealthExaminationSystem.Core.Company
{
    /// <summary>
    /// 单位预约登记
    /// </summary>
    public class TjlClientReg : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 单位信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey(nameof(ClientInfo))]
        public virtual Guid ClientInfoId { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        [Required]
        public virtual TjlClientInfo ClientInfo { get; set; }

        /// <summary>
        /// 单位分组信息
        /// </summary>
        public virtual ICollection<TjlClientTeamInfo> ClientTeamInfo { get; set; }

        /// <summary>
        /// 单位预约 人员
        /// </summary>
        public virtual ICollection<TjlCustomerReg> CustomerReg { get; set; }

        /// <summary>
        /// 收费记录
        /// </summary>
        public virtual ICollection<TjlMReceiptInfo> MReceiptInfo { get; set; }
        /// <summary>
        /// 应收费用记录
        /// </summary>
        public virtual ICollection<TjlMcusPayMoney> McusPayMoney { get; set; }
        /// <summary>
        /// 申请单
        /// </summary>
        public virtual ICollection<TjlApplicationForm> ApplicationForm { get; set; }
        /// <summary>
        /// 预约编码
        /// </summary>
        public virtual string ClientRegBM { get; set; }

        /// <summary>
        /// 预约次数
        /// </summary>
        public virtual int? ClientRegNum { get; set; }

        /// <summary>
        /// 预约人数
        /// </summary>
        public virtual int? RegPersonCount { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndCheckDate { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(2048)]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 预约描述
        /// </summary>
        [StringLength(1024)]
        public virtual string RegInfo { get; set; }

        /// <summary>
        /// 单位负责人 默认单位负责人
        /// </summary>
        [StringLength(64)]
        public virtual string linkMan { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        [ForeignKey("user")]
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 客服专员 默认创建人
        /// </summary>
        public virtual User user { get; set; }

        /// <summary>
        /// 是否盲检 1正常2盲检
        /// </summary>
        public virtual int BlindSate { get; set; }

        /// <summary>
        /// 封帐状态 1已封帐 2未封帐
        /// </summary>
        public virtual int FZState { get; set; }

        /// <summary>
        /// 封账时间
        /// </summary>        
        public virtual DateTime? FZTime { get; set; }

        /// <summary>
        /// 锁定状态 1锁定2未锁定
        /// </summary>
        public virtual int SDState { get; set; }

        /// <summary>
        /// 确认状态（财务确认）
        /// </summary>
        public virtual int? ConfirmState { get; set; }

        /// <summary>
        /// 是否控上传云端1上传2不上传
        /// </summary>
        public virtual int ControlDate { get; set; }

        /// <summary>
        /// 是否免费
        /// </summary>
        public bool? IsFree { get; set; }

        /// <summary>
        /// 到检人数
        /// </summary>

        public virtual int? CheckNumber { get; set; }

        /// <summary>
        /// 总检人数
        /// </summary>
        public virtual int? GeneralNumber { get; set; }

        /// <summary>
        /// 单位状态 1.正常2.散检单位
        /// </summary>
        public virtual int ClientSate { get; set; }

        /// <summary>
        /// 单位状态：1.已完成；2未完成
        /// </summary>
        public virtual int? ClientCheckSate { get; set; }

        /// <summary>
        /// 单位预约分组登记项目
        /// </summary>
        [InverseProperty(nameof(TjlClientTeamRegitem.ClientReg))]
        public virtual ICollection<TjlClientTeamRegitem> ClientTeamRegItems { get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        public virtual PersonnelCategory PersonnelCategory { get; set; }

        /// <summary>
        /// 人员类别标识
        /// </summary>
        [ForeignKey(nameof(PersonnelCategory))]
        public Guid? PersonnelCategoryId { get; set; }


        /// <summary>
        /// 柜子号
        /// </summary>
        [StringLength(32)]
        public virtual string CusCabitBM { get; set; }

        /// <summary>
        /// 存入状态
        /// </summary>        
        public virtual int? CusCabitState { get; set; }

        /// <summary>
        /// 存入时间
        /// </summary>        
        public virtual DateTime? CusCabitTime { get; set; }
        /// <summary>
        /// 结算状态
        /// </summary>        
        public virtual int? JSState { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>        
        public virtual DateTime? JStTime { get; set; }

        /// <summary>
        /// 结算人标识
        /// </summary>
        [ForeignKey("JSuser")]
        public virtual long? JSUserId { get; set; }

        /// <summary>
        /// 结算人标识
        /// </summary>
        public virtual User JSuser { get; set; }
        /// <summary>
        /// <summary>
        /// 数据行版本号
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// 所属院区
        /// </summary>
        public virtual int? HospitalArea { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <summary>
        /// 出报告天数
        /// </summary>
        public int? ReportDays { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}