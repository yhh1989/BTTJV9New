using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 体检人预约危害因素记录表
    /// </summary>
    public class TjlCustomerRegRiskFactors : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 体检人id
        /// </summary>
        [ForeignKey(nameof(CustomerReg))]
        public virtual Guid? CustomerRegID { get; set; }

        /// <summary>
        /// 危害因素id
        /// </summary>
        [ForeignKey(nameof(RiskFactor))]
        public virtual Guid? RiskFactorId { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual RiskFactor RiskFactor { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}