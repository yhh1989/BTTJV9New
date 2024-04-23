using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
 public   class INTJSQDto
    {
        /// <summary>
        /// 申请单号
        /// </summary>
        public string SQDH { get; set; }

        /// <summary>
        /// 申请状态1收费2退费
        /// </summary>
        public int? SQSTATUS { get; set; }

        /// <summary>
        /// 折后价格
        /// </summary>
        public decimal? PriceAfterDis { get; set; }


        /// <summary>
        /// 病人收费号
        /// </summary>
        public string BRSFH { get; set; }

        /// <summary>
        /// 病人发票
        /// </summary>
        public string BRFPH { get; set; }

    }
}
