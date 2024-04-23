﻿using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmCabinet))]
#endif
    public class TbmCabinetDto : EntityDto<Guid>
    {
        /// <summary>
        /// 行类型
        /// </summary>       
        public virtual int? HType { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>      
        public virtual int? WType { get; set; }
        /// <summary>
        /// 行个数
        /// </summary>  
        public virtual int? HCont { get; set; }

        /// <summary>
        /// 列个数
        /// </summary>
        public virtual int? WCont { get; set; }
        /// <summary>
        /// 份数/格
        /// </summary>     
        public virtual int? GCont { get; set; }
    }
}
