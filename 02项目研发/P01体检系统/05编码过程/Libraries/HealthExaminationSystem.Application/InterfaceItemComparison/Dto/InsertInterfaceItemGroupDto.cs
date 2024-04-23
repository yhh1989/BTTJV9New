﻿using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif

using System;
using System.ComponentModel.DataAnnotations;


namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto
{ /// <summary>
  /// 项目对应
  /// </summary>
#if !Proxy
    [AutoMap(typeof(TdbInterfaceItemGroupComparison))]
#endif
    public class InsertInterfaceItemGroupDto : EntityDto<Guid>
    {

        /// <summary>
        /// 科室ID
        /// </summary>      
        public virtual Guid DepartmentId { get; set; }


        /// <summary>
        /// 组合ID
        /// </summary>     
        public virtual Guid ItemGroupId { get; set; }


        /// <summary>
        /// 对应项目ID
        /// </summary>
        public virtual string ObverseItemId { get; set; }

        /// <summary>
        /// 对应项目名称?
        /// </summary>
        [StringLength(64)]
        public virtual string ObverseItemName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(1024)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 仪器型号
        /// </summary>
        [StringLength(64)]
        public virtual string InstrumentModelNumber { get; set; }
       

    }
}
