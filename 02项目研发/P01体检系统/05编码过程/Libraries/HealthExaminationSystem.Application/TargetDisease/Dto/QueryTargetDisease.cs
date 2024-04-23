using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto
{
    public class QueryTargetDisease
    {
        /// <summary>
        /// 危害因素标识
        /// </summary>
        public Guid RiskFactorId { get; set; }

        /// <summary>
        /// 岗位类别标识
        /// </summary>
        public Guid JobCategoryId { get; set; }
        /// <summary>
        /// 危害因素标识
        /// </summary>
        public List<Guid> RiskFactorIdList { get; set; }
    }
}
