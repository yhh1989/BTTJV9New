using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif
using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto
{
    /// <summary>
    /// 项目对应
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TdbInterfaceEmployeeComparison))]
#endif
    public class InsertInterfaceEmpDto : EntityDto<Guid>
    {
     
        /// <summary>
        /// 操作人员ID:int?
        /// </summary>       
        public virtual long EmployeeId { get; set; }

        /// <summary>
        /// 操作人员名称
        /// </summary>
        [StringLength(64)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 对应操作人员ID
        /// </summary>
        public virtual string ObverseEmpId { get; set; }

        /// <summary>
        /// 对应操作人员名称
        /// </summary>
        [StringLength(64)]
        public virtual string ObverseEmpName { get; set; }

    }
}
