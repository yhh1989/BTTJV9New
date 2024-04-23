using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Abp.AutoMapper;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(PersonnelCategory))]
#endif
    public class PersonTypeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否免费
        /// </summary>
        public bool IsFree { get; set; }
    }
}
