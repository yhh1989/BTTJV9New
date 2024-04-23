using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto
{
   public class HisSumDto
    {
       
        /// <summary>
        /// 预约编码
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 登记日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }

        /// <summary>
        /// 检查年份
        /// </summary>
        public string ChekDateYear { get; set; }
        /// <summary>
        /// 检查年份
        /// </summary>
        public DateTime? ChekDateYear1 { get; set; }

        /// <summary>
        /// 检查诊断
        /// </summary>
        public string diagnosis { get; set; }

        /// <summary>
        /// 汇总
        /// </summary>
        public virtual string Sum { get; set; }
    }
}
