using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
   public class OccQuestionFamilyrucan
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 家族史疾病
        /// </summary>
        public virtual string IllName { get; set; }
        /// <summary>
        /// 疾病关系人
        /// </summary>
        public virtual string relatives { get; set; }

        /// <summary>
        /// 问卷Id
        /// </summary>
        public virtual Guid? OccQuestionnaireBMId { get; set; }
    }
}
