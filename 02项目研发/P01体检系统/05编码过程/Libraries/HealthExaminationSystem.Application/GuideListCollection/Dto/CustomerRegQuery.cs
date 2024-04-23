using System;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.GuideListCollection.Dto
{
#if Application
    [AutoMap(typeof(TjlCustomerReg))]
#endif
    public class CustomerRegQuery : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人
        /// </summary>
        public virtual CustomerQuery Customer { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual Guid? ClientInfoId { get; set; }

        /// <summary>
        /// 导引单号 单位累加、个人当天累加
        /// </summary>
        public virtual int? GuidanceNum { get; set; }
    }
}