using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCusCabit))]
#endif
    public class TjlCusCabitDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>      
        public virtual Guid? CustomerRegId { get; set; }
       

        /// <summary>
        /// 单位预约信息外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
      
        public virtual Guid? ClientRegId { get; set; }

       
        /// <summary>
        /// 报个状态1个报2团报
        /// </summary>

        public int? ReportState { get; set; }
        /// <summary>
        /// 柜子号
        /// </summary>
        public string CabitName { get; set; }
        /// <summary>
        /// 取出状态1存入2取出
        /// </summary>
        public int? GetState { get; set; }
      
    }
}
