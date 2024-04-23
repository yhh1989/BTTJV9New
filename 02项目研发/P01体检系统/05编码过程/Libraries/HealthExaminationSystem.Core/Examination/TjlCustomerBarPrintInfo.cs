using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 条码打印记录
    /// </summary>
    public class TjlCustomerBarPrintInfo : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 打印条码集合
        /// </summary>
        public virtual ICollection<TjlCustomerBarPrintInfoItemGroup> CustomerBarPrintInfo { get; set; }

        /// <summary>
        /// 体检人标识
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid? CustomerReg_Id { get; set; }
        
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 条码标识
        /// </summary>
        [ForeignKey("BarSettings")]
        public virtual Guid? BarSettingsId { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        public virtual TbmBarSettings BarSettings { get; set; }
        /// <summary>
        /// 档案号
        /// </summary>
        public virtual string ArchivesNum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 条码名称
        /// </summary>
        [StringLength(1000)]
        public virtual string BarName { get; set; }

        /// <summary>
        /// 条码编号
        /// </summary>
        [StringLength(32)]
        public virtual string BarNumBM { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public virtual DateTime? BarPrintTime { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public virtual int? BarPrintCount { get; set; }

        /// <summary>
        /// 已抽血
        /// </summary>
        [Column("HaveBlood")]
        public bool HaveBlood { get; set; }

        /// <summary>
        /// 抽血时间
        /// </summary>
        [Column("BloodTime")]
        public DateTime? BloodTime { get; set; }

        /// <summary>
        /// 抽血人标识
        /// </summary>
        [ForeignKey(nameof(BloodUser))]
        [Column("BloodUserId")]
        public long? BloodUserId { get; set; }

        /// <summary>
        /// 抽血人
        /// </summary>
        public virtual User BloodUser { get; set; }



        /// <summary>
        /// 已接收
        /// </summary>
        [Column("HaveReceive")]
        public bool HaveReceive { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        [Column("ReceiveTime")]
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 接收人标识
        /// </summary>
        [ForeignKey(nameof(ReceiveUser))]
        [Column("ReceiveUserId")]
        public long? ReceiveUserId { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public virtual User ReceiveUser { get; set; }

        /// <summary>
        /// 已送检
        /// </summary>
        [Column("HaveSend")]
        public bool HaveSend { get; set; }
        /// <summary>
        /// 送检时间
        /// </summary>
        [Column("SendTime")]
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 送检人标识
        /// </summary>
        [ForeignKey(nameof(SendUser))]
        [Column("SendUserId")]
        public long? SendUserId { get; set; }

        /// <summary>
        /// 送检人
        /// </summary>
        public virtual User SendUser { get; set; }


        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}