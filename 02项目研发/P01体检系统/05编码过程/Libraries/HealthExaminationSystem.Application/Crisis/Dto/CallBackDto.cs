using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Crisis;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    /// <summary>
    /// 回访dto
    /// </summary>
#if !Proxy
    [AutoMapFrom(typeof(TjlCustomerServiceCallBack))]
#endif
   
    public class CallBackDto : EntityDto<Guid>
    {
        /// <summary>
        /// 危急值记录Id
        /// </summary>
        public Guid TjlCrisisSetId { get; set; }
        /// <summary>
        /// 危急值项目
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// 危急值结果
        /// </summary>
        public string ItemResultChar { get; set; }
        /// <summary>
        /// 回访方式 微信0 电话1 短信2 到店3
        /// </summary>
        public int CallBackType { get; set; }
        /// <summary>
        /// 回访内容
        /// </summary>
        public string CallBacKContent { get; set; }
        /// <summary>
        /// 回访时间
        /// </summary>
        public DateTime CallBacKDate { get; set; }
        /// <summary>
        /// 回访状态 完成0 关闭1
        /// </summary>
        public int? CallBackState { get; set; }
        /// <summary>
        /// 回访人
        /// </summary>
        public string CallBackName { get; set; }
    }
}
