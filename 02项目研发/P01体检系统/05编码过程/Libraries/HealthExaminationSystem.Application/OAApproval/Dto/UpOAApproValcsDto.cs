using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OAApproval.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlOAApproValcs))]
#endif
    public class UpOAApproValcsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 批示
        /// </summary>
        [StringLength(500)]
        public virtual string Comments { get; set; }

        /// <状态>
        /// 审批状态 0未审批 1已审批2已拒绝
        /// </summary>

        public virtual int? AppliState { get; set; }
        /// <summary>
        /// 审批时间
        /// </summary>

        public virtual DateTime? ApprovalTime { get; set; }
    }
}
