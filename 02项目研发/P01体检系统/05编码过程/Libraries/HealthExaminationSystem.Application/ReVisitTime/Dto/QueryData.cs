using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime.Dto
{
    public class QueryData
    {
        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual Guid? ItemGroupBM_Id { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public virtual Guid? Id { get; set; }
        /// <summary>
        /// 单位预约id
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }
    }
}
