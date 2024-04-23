﻿using Abp.Application.Services.Dto;

#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto
{
#if !Proxy  
    [AutoMap(typeof(TbmOccDictionary))]
#endif
    public class ParentHazardFactorDto : EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(500)]
        public virtual string HelpChar { get; set; }
    }
}
