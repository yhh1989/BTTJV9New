using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    /// <summary>
    /// 设置危急值Dto
    /// </summary>
    public class SetCrisisDto
    {
        /// <summary>
        /// 危急值信息
        /// </summary>
        public List<TjlCrisisSetDto> CrisisInfos;
        /// <summary>
        /// 预约Id
        /// </summary>
        public Guid CustomerRegId;
    }
}
