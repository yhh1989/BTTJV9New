#if Application
using AutoMapper;
#endif
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
   public   class ReportQuesCareerHistoryDto
    {

        /// <summary>
        /// 体检人预约主键
        /// </summary>       
        public virtual Guid? CustomerRegBMId { get; set; }       
        

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StarTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string FormatStarTime
        {
            get
            {
                var Star = StarTime.HasValue ? StarTime.Value.ToString() : "无";
                return Star;
            }
        }
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string FormatEndTime
        {
            get
            {
                var End = EndTime.HasValue ? EndTime.Value.ToString() : "无";
                return End;
            }
        }

        /// <summary>
        /// 工作单位
        /// </summary>
        [StringLength(100)]
        public virtual string WorkClient { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(100)]
        public virtual string WorkName { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(100)]
        public virtual string WorkType { get; set; }

        /// <summary>
        /// 工龄
        /// </summary>

        public virtual decimal? WorkYears { get; set; }

        /// <summary>
        /// 工龄单位
        /// </summary>
        public virtual string UnitAge { get; set; }

        /// <summary>
        /// 危害因素
        /// </summary>        
        public virtual string HisHazards { get; set; }
    
        /// <summary>
        /// 防护措施
        /// </summary>       
        public virtual string HisProtectives { get; set; }
        /// <summary>
        /// 报告签名
        /// </summary>
#if !Proxy
                [IgnoreMap]
#endif
        public virtual string 受检人签名 { get; set; }


        /// <summary>
        /// 工龄文本
        /// </summary>

        public virtual string StrWorkYears { get; set; }

    }
}
