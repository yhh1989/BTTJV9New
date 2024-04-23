using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
    public class OutHisValuesDto
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual Guid? ItemId { get; set; }

        /// <summary>
        /// 项目结果
        /// </summary>
        public virtual string ItemValue { get; set; }
    }
}
