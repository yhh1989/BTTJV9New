using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 抽血记录
    /// </summary>
    public class TjlCustomerBloodNum : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 抽血时间
        /// </summary>
        public virtual DateTime? BloodDate { get; set; }

        /// <summary>
        /// 抽血号
        /// </summary>
        [MaxLength(16)]
        public virtual string BloodNum { get; set; }
        /// <summary>
        /// 预约记录外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid CustomerRegId { get; set; }


        /// <summary>
        /// 预约id
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }


        /// <summary>
        /// 抽血人
        /// </summary>
        public virtual User EmployeeBM { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        [MaxLength(64)]
        public virtual string Ip { get; set; }

        /// <summary>
        /// 抽血状态 1已抽血2已取消
        /// </summary>
        public virtual int? BloodSate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}