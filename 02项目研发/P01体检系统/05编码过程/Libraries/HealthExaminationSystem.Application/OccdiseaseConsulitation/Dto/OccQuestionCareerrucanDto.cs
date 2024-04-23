using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
   public  class OccQuestionCareerrucanDto
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
      
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StarTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>

        public virtual string WorkClient { get; set; }
        /// <summary>
        /// 车间
        /// </summary>

        public virtual string WorkName { get; set; }

        /// <summary>
        /// 工种

        public virtual string WorkType { get; set; }

        /// <summary>
        /// 工龄
        /// </summary>

        public virtual int WorkYears { get; set; }

    }
}
