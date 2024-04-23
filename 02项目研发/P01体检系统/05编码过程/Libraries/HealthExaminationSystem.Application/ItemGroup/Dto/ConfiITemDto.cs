using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto
{
 public   class ConfiITemDto
    {
        /// <summary>
        /// 已有组合
        /// </summary>
        public virtual List<Guid> HasGroupIds { get; set; }
        /// <summary>
        /// 已选组合
        /// </summary>
        public virtual List<Guid> CheckGroupIds { get; set; }
    }
}
