using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
    public class QueryGiveUpDto
    {
        /// <summary>
        /// 体检人ID
        /// </summary>
        public Guid CustomerRegId { get; set; }

        /// <summary>
        /// 项目组ID
        /// </summary>
        public Guid CustomerItemGroupId { get; set; }
    }
}
