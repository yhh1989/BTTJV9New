using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OccDayStatic.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlOccCustomerSum))]
#endif
    public class OutOccDayDto
    {
        public DateTime? startDate { get; set; }
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual string LoginDate { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int DayCount { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int NumberCount { get; set; }
        /// <summary>
        /// 年度
        /// </summary>
        public string YearTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndCheckDate { get; set; }

        /// <summary>
        /// 1天
        /// </summary>
        public int? OneDay { get; set; }
        /// <summary>
        /// 2天
        /// </summary>
        public int? TwoDay { get; set; }
        /// <summary>
        /// 3天
        /// </summary>
        public int? ThreeDay { get; set; }
        /// <summary>
        /// 四天
        /// </summary>
        public int? FourDay { get; set; }
        /// <summary>
        /// 五天
        /// </summary>
        public int? FiveDay { get; set; }
        /// <summary>
        /// 六天
        /// </summary>
        public int? SixDay { get; set; }
        /// <summary>
        /// 七天
        /// </summary>
        public int? SevenDay { get; set; }
        /// <summary>
        /// 八天
        /// </summary>
        public int? EightDay { get; set; }
        /// <summary>
        /// 九天
        /// </summary>
        public int? NineDay { get; set; }
        /// <summary>
        /// 十天
        /// </summary>
        public int? TenDay { get; set; }
        /// <summary>
        /// 十一天
        /// </summary>
        public int? ElevenDay { get; set; }
        /// <summary>
        /// 十二天
        /// </summary>
        public int? TelveDay { get; set; }
        /// <summary>
        /// 十三天
        /// </summary>
        public int? ThirteenDay { get; set; }
        /// <summary>
        /// 十四天
        /// </summary>
        public int? FourteenDay { get; set; }
        /// <summary>
        /// 十五天
        /// </summary>
        public int? FifteenDay { get; set; }
        /// <summary>
        /// 十六天
        /// </summary>
        public int? SixteenDay { get; set; }
        /// <summary>
        /// 十七天
        /// </summary>
        public int? SeventeenDay { get; set; }
        /// <summary>
        /// 十八天
        /// </summary>
        public int? EighteenDay { get; set; }
        /// <summary>
        /// 十九天
        /// </summary>
        public int? NineteenDay { get; set; }
        /// <summary>
        /// 二十天
        /// </summary>
        public int? TwentyDay { get; set; }
        /// <summary>
        /// 二十一天
        /// </summary>
        public int? TwentyOneDay {get;set;}
        /// <summary>
        /// 二十二天
        /// </summary>
        public int? TwentyTwoDay { get; set; }
        /// <summary>
        /// 二十三天
        /// </summary>
        public int? TwentythreeDay { get; set; }
        /// <summary>
        /// 二十四天
        /// </summary>
        public int? TwentyFourDay { get; set; }
        /// <summary>
        /// 二十五天
        /// </summary>
        public int? TwentyFiveDay { get; set; }
        /// <summary>
        /// 二十六天
        /// </summary>
        public int? TwentySixDay { get; set; }
        /// <summary>
        /// 二十七天
        /// </summary>
        public int? TwentySevenDay { get; set; }
        /// <summary>
        /// 二十八天
        /// </summary>
        public int? TwentyEightDay { get; set; }
        /// <summary>
        /// 二十九
        /// </summary>
        public int? TwentyNineDay { get; set; }
        /// <summary>
        /// 三十天
        /// </summary>
        public int? ThirtyDay { get; set; }
        /// <summary>
        /// 三十一天
        /// </summary>
        public int? ThirtyOneDay { get; set; }
    }
}
