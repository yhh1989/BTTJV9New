using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class Refee_chargeDto
    {
        /// <summary>
        /// 病人id
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 结帐id
        /// </summary>
        public string sett_id { get; set; }

        /// <summary>
        /// 原结帐id
        /// </summary>
        public string old_sett_id { get; set; }
        
        /// <summary>
        ///总费用
        /// </summary>
        public decimal? total_money { get; set; }

        /// <summary>
        ///结算时间|yyyy-mm-dd hh24:mi:ss
        /// </summary>
        public string pay_time { get; set; }
        /// <summary>
        /// 结算信息段
        /// </summary>
        public List<sett_infoDto> sett_info { get; set; }
        /// <summary>
        /// 费用交易信息段
        /// </summary>
        public List<fee_infoDto> fee_info { get; set; }
    }
}
