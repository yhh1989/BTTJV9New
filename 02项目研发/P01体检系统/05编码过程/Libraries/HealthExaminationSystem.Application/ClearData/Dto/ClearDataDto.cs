using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ClearData.Dto
{
    /// <summary>
    /// 删除业务数据
    /// </summary>
    public class ClearDataDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>

        public virtual string EndTime { get; set; }

        

    }
}
