﻿using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
    /// <summary>
    /// 目标疾病-症状
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmOccDisease))]
#endif
    public class TbmOccDiseaseDto : EntityDto<Guid>
    {
        /// <summary>
        /// 职业健康Id
        /// </summary>

        public virtual Guid? OccDiseasesId { get; set; }

        /// <summary>
        /// 标准编号
        /// </summary>      
        public virtual string StandardNo { get; set; }

        /// <summary>
        /// 职业健康标准
        /// </summary>
        public virtual string StandardName { get; set; }
        /// <summary>
        /// 是否默认1默认0否
        /// </summary>     
        public virtual int IsShow { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public virtual string HelpChar { get; set; }
        /// <summary>
        /// <summary>
        /// 名称
        /// </summary>

        public virtual string Text { get; set; }
    }
}
