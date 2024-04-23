using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlLoginList))]
#endif
    public class LoginlistDto : EntityDto<Guid>
    {
        /// <summary>
        /// 开单医生Id
        /// </summary>    
        public virtual long UserId { get; set; }
        /// <summary>
        /// 是否有删除数据
        /// </summary>    
#if Application
        [IgnoreMap]
#endif
        public virtual int? hasDel { get; set; }
    }
}
