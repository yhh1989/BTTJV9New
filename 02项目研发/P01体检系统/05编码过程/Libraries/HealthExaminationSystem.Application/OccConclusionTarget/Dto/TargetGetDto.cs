using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionTarget.Dto
{
  public  class TargetGetDto:EntityDto<Guid>
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }


        /// <summary>
        /// 年度
        /// </summary>
        public string YearTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndCheckDate { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }

        /// <summary>
        /// 单位分组名称
        /// </summary>
        public virtual string TeamName { get; set; }
    }
}
