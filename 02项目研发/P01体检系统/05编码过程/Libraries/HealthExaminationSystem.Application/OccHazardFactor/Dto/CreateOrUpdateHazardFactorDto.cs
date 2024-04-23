﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmOccHazardFactor))]
#endif
    public class CreateOrUpdateHazardFactorDto : EntityDto<Guid>
    {
        /// <summary>
        /// CAS编码
        /// </summary>
        [StringLength(50)]
        public virtual string CASBM { get; set; }

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
        /// 父级单位标识（危害因素分类）
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        ///// <summary>
        ///// 父级单位
        ///// </summary>
        //public virtual string Parent { get; set; }
        
        /// <summary>
        /// 类别名称
        /// </summary>
        [StringLength(500)]
        public virtual string Category { get; set; }

        /// <summary>
        ///父级名称
        /// </summary>
        //public virtual string ParentName { get; set; }
        /// <summary>
        /// 防护措施
        /// </summary>
        //public virtual ICollection<HazardFactorsProtective> Protectivis { get; set; }
    }
}
