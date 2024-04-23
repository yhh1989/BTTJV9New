using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 复查操作
    /// </summary>
    public class TjlCustomerDoCwOrk : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegIBM { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual TbmDepartment DepartmentBM { get; set; }

        /// <summary>
        /// 组合ID
        /// </summary>
        public virtual TbmItemGroup ItemGroupBM { get; set; }


        /// <summary>
        /// 疾病名称
        /// </summary>
        [MaxLength(64)]
        public virtual string IlName { get; set; }

        /// <summary>
        /// 疾病ID
        /// </summary>
        public virtual int? IINameID { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public virtual User InspectEmployeeID { get; set; }

        /// <summary>
        /// 审核者
        /// </summary>
        public virtual int? CheckEmployeeID { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        [MaxLength(512)]
        public virtual string Result { get; set; }

        /// <summary>
        /// 复查时间
        /// </summary>
        public virtual DateTime? RDateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int? State { get; set; }

        /// <summary>
        /// 回访时间
        /// </summary>
        public virtual DateTime? KFDate { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}