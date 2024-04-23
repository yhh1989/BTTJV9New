using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReSultStatus.Dto
{
   public class ReSultCusDepartMentDto
    {
        /// <summary>
        ///  项目
        /// </summary>
        public List<Guid> reSultCusItems { get; set; }
        /// <summary>
        ///  组合
        /// </summary>
        public List<Guid> reSultCusGroups { get; set; }
        /// <summary>
        ///  科室
        /// </summary>
        public List<Guid> reSultCusDepart { get; set; }
    }
}
