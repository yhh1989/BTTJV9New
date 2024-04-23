using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper.Dto
{
    /// <summary>
    /// 日报月报查询类
    /// </summary>
   public class QuerySunMoon
    {
        /// <summary>
        /// 收费日期启
        /// </summary>
        public DateTime? ChargeDateStart { get; set; }
        /// <summary>
        /// 收费日期止
        /// </summary>
        public DateTime? ChargeDateEnd { get; set; }
        /// <summary>
        /// 人员类别。1个人2单位3全部
        /// </summary>
        public int PersonnelCategory { get; set; }
        /// <summary>
        /// 收费人
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public Guid? Client { get; set; }
    }
}
