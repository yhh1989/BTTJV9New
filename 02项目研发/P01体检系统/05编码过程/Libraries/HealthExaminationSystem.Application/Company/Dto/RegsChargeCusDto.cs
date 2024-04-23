using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlClientReg))]
#endif
    public class RegsChargeCusDto : EntityDto<Guid>
    {
        public DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndCheckDate { get; set; }
    }
}