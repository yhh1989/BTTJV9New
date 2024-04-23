using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
   public class OccQustionSymptomrucan:EntityDto<Guid>
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
    }
}
