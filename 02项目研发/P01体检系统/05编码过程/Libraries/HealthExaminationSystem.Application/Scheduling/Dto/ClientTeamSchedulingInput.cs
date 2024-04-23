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
    public class ClientTeamSchedulingInput : EntityDto<Guid>
    {
        /// <summary>
        /// 单位预约表外键
        /// </summary>
        public virtual Guid ClientRegId { get; set; }

        /// <summary>
        /// 单位（登记/预约）分组表外键
        /// </summary>
        public virtual Guid ClientTeamInfoId { get; set; }

        /// <summary>
        /// 排期日期
        /// </summary>
        [Required]
        public virtual DateTime? ScheduleDate { get; set; }

        /// <summary>
        /// 总人数
        /// </summary>
        public virtual int TotalNumber { get; set; }
        /// <summary>
        /// 男人数
        /// </summary>
        public virtual int? ManNumber { get; set; }
        /// <summary>
        /// 女人数
        /// </summary>
        public virtual int? WomanNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }
    }
}
