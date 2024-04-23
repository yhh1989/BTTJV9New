using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{

#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class UpClientMZDto : EntityDto<Guid>
    {
        /// <summary>
        /// 预约编码
        /// </summary>
        public string ClientRegBM { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }

    }
}
