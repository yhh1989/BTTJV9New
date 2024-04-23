#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
    /// <summary>
    /// 防护措施
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlOccQuesHisProtective))]
#endif
    public class OccdieaseProtectDto
    {
        /// <summary>
        /// 职业史Id
        /// </summary>
        
        public virtual Guid? OccCareerHistoryBMId { get; set; }


        /// <summary>
        /// 名称
        /// </summary>

        public virtual string Text { get; set; }

        /// <summary>
        /// 编码
        /// </summary>

        public virtual string BM { get; set; }

       
    }
}
