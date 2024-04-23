using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public  class fee_infoDto
    {
        /// <summary>
        /// 单据号
        /// </summary>
        public string receipt_no { get; set; }

        /// <summary>
        /// 费用发生时间|yyyy-mm-dd hh24:mi:ss
        /// </summary>
        public string receipt_time { get; set; }

        /// <summary>
        /// 开单科室id
        /// </summary>
        public string apply_dept_id { get; set; }


        /// <summary>
        /// 开单科室名称
        /// </summary>
        public string apply_dept_name { get; set; }

        /// <summary>
        /// 	开单医生id
        /// </summary>
        public string doctor_id { get; set; }

        /// <summary>
        /// 	费别|值域通过S1000服务获取
        /// </summary>
        public string fees_type { get; set; }


        /// <summary>
        /// 应收金额|数字
        /// </summary>
        public decimal? should_money { get; set; }

        /// <summary>
        /// 实收金额|数字
        /// </summary>
        public decimal? actual_money { get; set; }

        /// <summary>
        /// 操作员id
        /// </summary>
        public string oprtr_id { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string oprtr_name { get; set; }


        /// <summary>
        /// 操作时间,费用登记时间|yyyy-mm-dd hh24:mi:ss
        /// </summary>
        public string oprtr_time { get; set; }

     

    }
}
