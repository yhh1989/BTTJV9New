using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    public class IndividualitiesDto
    {
        /// <summary>
        /// 项目组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal? GroupsMoney { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public virtual int? Counts { get; set; }
        /// <summary>
        /// 总原价
        /// </summary>
        public virtual decimal? CountGroupsMoney { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public virtual decimal? SholdMoney { get; set; }
        /// <summary>
        /// 实际收费金额
        /// </summary>
        public virtual decimal? SJMoney { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal? Discount { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }
       
       // public List<ClientDepartDto> departlist { get; set; }

    }
}
