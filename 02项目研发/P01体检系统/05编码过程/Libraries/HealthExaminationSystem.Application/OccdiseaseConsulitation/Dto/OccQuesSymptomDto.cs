#if !Proxy
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlOccQuesSymptom))]
#endif
    public class OccQuesSymptomDto:EntityDto<Guid>
    {
         
        /// <summary>
        /// 体检人预约主键
        /// </summary>

        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        ///症状名称
        /// </summary>

        public virtual string Name { get; set; }
        /// <summary>
        /// 症状分类
        /// </summary>
  
        public virtual string Type { get; set; }
        /// <summary>
        /// 症状程度
        /// </summary>
        public virtual string Degree { get; set; }

        /// <summary>
        /// 出现时间
        /// </summary>     
        public virtual DateTime? StarTime { get; set; }
    }
}
