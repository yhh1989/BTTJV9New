using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
   public  class SearchSFTypeDto 
    {
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 查询名称
        /// </summary>
        [StringLength(128)]
        public string SearchName { get; set; }
        /// <summary>
        /// 最小金额
        /// </summary>
        public decimal? MinMoney { get; set; }

        /// <summary>
        /// 最大金额
        /// </summary>
        public decimal? MaxMoney { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public Guid? ClientRegID { get; set; }
        /// <summary>
        /// 查询用户种类1.登记人2.收费人3.介绍人
        /// </summary>
        public int? UserType { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public long?  UserID { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(128)]
        public string LinkName { get; set; }

        /// <summary>
        /// 大科室
        /// </summary>
        public int? DepatType { get; set; }
        /// <summary>
        /// 查询种类0.已收费1.已登记
        /// </summary>
        public int? SeachType { get; set; }

        /// <summary>
        /// 1统计团体自费
        /// </summary>
        public int? TTMoney { get; set; }



    }
}
