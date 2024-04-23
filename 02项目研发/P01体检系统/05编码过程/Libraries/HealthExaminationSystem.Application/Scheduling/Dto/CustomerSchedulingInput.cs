using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{
#if !Proxy
    [AutoMapTo(typeof(TjlScheduling))]
#endif
    public class CustomerSchedulingInput : EntityDto<Guid>
    {
        /// <summary>
        /// 单位（登记/预约）分组表外键
        /// </summary>
        public virtual Guid CustomerRegId { get; set; }

        /// <summary>
        /// 排期日期
        /// </summary>
        [Required]
        public virtual DateTime? ScheduleDate { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }
    }
}
