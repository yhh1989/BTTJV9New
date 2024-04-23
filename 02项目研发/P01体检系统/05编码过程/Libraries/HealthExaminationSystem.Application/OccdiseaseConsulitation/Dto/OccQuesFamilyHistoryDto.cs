using Abp.Application.Services.Dto;
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
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlOccQuesFamilyHistory))]
#endif
    public class OccQuesFamilyHistoryDto:EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>

        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 问卷Id
        /// </summary>
   
        public virtual Guid? OccQuestionnaireBMId { get; set; }


        /// <summary>
        /// 家族史疾病
        /// </summary>

        public virtual string IllName { get; set; }
        /// <summary>
        /// 疾病关系人
        ///  </summary>
        public virtual string relatives { get; set; }


    }
}
