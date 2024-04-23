using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlClientReg))]
#endif
    public class SimpleClientRegDto : EntityDto<Guid>
    {
        public virtual SimpleClientInfoDto ClientInfo { get; set; }

        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual string ClientRegBM { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime EndCheckDate { get; set; }

    }
}
