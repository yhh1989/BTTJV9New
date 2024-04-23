#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto 
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCusVisit))]
#endif
    public class CusVisitDto
    {
      
        
        /// <summary>
        /// 预约标识
        /// </summary> 
        public virtual Guid CustomerRegID { get; set; }


        /// <summary>
        /// 回访状态0未回访1已回访3已取消
        /// </summary>
        public virtual int? VisitState { get; set; }

        /// <summary>
        ///回访日期
        /// </summary>
        public virtual DateTime? VisitDate { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>     
        public virtual string colour { get; set; }

        /// <summary>
        /// 备注
        /// </summary>    
        public virtual string remarks { get; set; }
    }
}
