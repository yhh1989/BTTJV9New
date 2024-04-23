#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class TjlCustomerRegDto
    {
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 导诊开始时间
        /// </summary>
        public virtual DateTime? NavigationStartTime { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        public virtual string TypeWork { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>

        public virtual string PostState { get; set; }
    }
}
