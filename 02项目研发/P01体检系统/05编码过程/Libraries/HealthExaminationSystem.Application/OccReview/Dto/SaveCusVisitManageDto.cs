#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto
{
    /// <summary> 
    /// 查询客户预约信息Dto 
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCusVisitManage))]
#endif
    public class SaveCusVisitManageDto : EntityDto<Guid>
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
        /// 回访方式1短信2电话
        /// </summary>      
        public virtual string VisitType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>     
        public virtual string remarks { get; set; }

        /// <summary>
        /// 回访人外键
        /// </summary>     
        public virtual long? VisitEmployeeId { get; set; }
    }
}
