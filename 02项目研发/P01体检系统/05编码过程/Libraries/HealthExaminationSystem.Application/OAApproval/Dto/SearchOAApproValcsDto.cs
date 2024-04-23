using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto
{
   public  class SearchOAApproValcsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位预约ID
        /// </summary>      
        public virtual Guid? ClientInfoId { get; set; }

        /// <summary>
        /// 申请开始时间
        /// </summary>

        public virtual DateTime? StarAppliTime { get; set; }
        /// <summary>
        /// 申请结束时间
        /// </summary>

        public virtual DateTime? EndAppliTime { get; set; }

        /// <summary>
        /// 创建开始时间
        /// </summary>

        public virtual DateTime? StarCreateTime { get; set; }
        /// <summary>
        /// 创建结束时间
        /// </summary>

        public virtual DateTime? EndCreateTime { get; set; }

        /// <状态>
        /// 审批状态 0未审批 1已审批2已拒绝
        /// </summary>

        public virtual int? AppliState { get; set; }



    }
}
