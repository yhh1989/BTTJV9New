using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.DailyNewspaper.Dto
{
    /// <summary>
    /// 体检人预约信息
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class SearchCustomerRegDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检ID
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
    }
}
