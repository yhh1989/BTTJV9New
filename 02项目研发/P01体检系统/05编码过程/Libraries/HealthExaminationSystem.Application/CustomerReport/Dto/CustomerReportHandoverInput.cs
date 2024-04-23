using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
    public class CustomerReportHandoverInput
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public virtual string Number { get; set; }
     
        /// <summary>
        /// 交接/发单人
        /// </summary>
        public virtual string Handover { get; set; }
        /// <summary>
        /// 发放/领单人
        /// </summary>
        public virtual string Receiptor { get; set; }
        /// <summary>
        /// 完成交接/发放
        /// </summary>
        public virtual bool? Complete { get; set; }

    }
}
