using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.DTO
{
   public  class InterfaceEmployeeComparisonDto : EntityDto<Guid>
    {
        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual SearchUserDto Employee { get; set; }

        /// <summary>
        /// 操作人员ID:int?
        /// </summary>
        [ForeignKey(nameof(Employee))]
        public virtual long EmployeeId { get; set; }

        /// <summary>
        /// 操作人员名称
        /// </summary>
        [MaxLength(64)]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 对应操作人员ID
        /// </summary>
        public virtual string ObverseEmpId { get; set; }

        /// <summary>
        /// 对应操作人员名称
        /// </summary>
        [MaxLength(64)]
        public virtual string ObverseEmpName { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
