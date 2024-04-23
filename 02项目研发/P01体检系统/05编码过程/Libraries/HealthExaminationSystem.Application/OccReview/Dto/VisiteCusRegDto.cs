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

namespace Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg),typeof(TjlCustomerSummarize))]
#endif
    public class VisiteCusRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检号
        /// </summary>      
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual VisiteCustomerDto Customer { get; set; }

        /// <summary>
        ///  回访状态0未回访1已回访3已取消
        /// </summary>
        public virtual int? VisitSate { get; set; }
        /// <summary>
        /// 回访人外键
        /// </summary>
    
        public virtual long? VisitEmployeeId { get; set; }
        /// <summary>
        /// 回访日期
        /// </summary>
        public DateTime? VisitTime { get; set; }
        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 复查短信 0未发送 1已发送
        /// </summary>
        public int? VisitMessageState { get; set; }
    }
}
