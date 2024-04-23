﻿using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmOccDisease))]
#endif
    public class OutOccDiseaseDto : EntityDto<Guid>
    {

        /// <summary>
        /// 字典类别，来自程序的枚举
        /// </summary>
        public virtual string Type { get; set; }

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
        /// <summary>
        /// 是否启用1启用0停用
        /// </summary>   
        public virtual int IsActive { get; set; }

#if Application
        [IgnoreMap]
#endif
        public virtual string IsActiveStatr
        {
            get
            {
                if (IsActive == 1)
                {
                    return "启用";
                }
                else
                {
                    return "停用";
                }
            }

        }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3072)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 父级单位标识（职业健康大类）
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 父级单位
        /// </summary>
        public virtual ParentDicDto Parent { get; set; }

        /// <summary>
        /// 职业健康标准
        /// </summary>
        public virtual ICollection<OccDiseaseStandardDto> TbmOccStandards { get; set; }

    }
}
