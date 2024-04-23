#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerDepSummary))]
#endif
    public class HisDepartSumDto
    {
        public virtual Guid? regId { get; set; }
        /// <summary>
        /// 预约编码
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 登记日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartOrder { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public virtual string DepartBM { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string DepartName { get; set; }

        /// <summary>
        /// 科室小节
        /// </summary>
        public virtual string DepartSum { get; set; }
    }
}
