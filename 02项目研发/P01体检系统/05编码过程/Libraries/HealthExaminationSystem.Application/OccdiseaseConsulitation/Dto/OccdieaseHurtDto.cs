
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
    /// 有害因素
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlOccQuesHisHazardFactors))]
#endif
    public class OccdieaseHurtDto
    {
        /// <summary>
        /// 职业史Id
        /// </summary>
        public virtual Guid? OccCareerHistoryBMId { get; set; }

        /// <summary>
        /// CAS编码
        /// </summary>
        public virtual string CASBM { get; set; }

       
        /// <summary>
        /// 危害因素名称
        /// </summary>
        public virtual string Text { get; set; }

    }
}
