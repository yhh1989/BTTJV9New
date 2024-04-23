using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{ /// <summary>
  /// 单位信息表
  /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlClientInfo))]
#endif
    public class TjlClientInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string ClientBM { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
    }
}