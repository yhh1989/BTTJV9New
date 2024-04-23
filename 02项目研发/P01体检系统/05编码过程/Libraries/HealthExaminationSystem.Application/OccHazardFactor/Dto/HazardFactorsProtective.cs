using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Abp.Application.Services.Dto;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto
{
    /// <summary>
    /// 目标疾病-症状
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmOccHazardFactorsProtective))]
#endif
    public class HazardFactorsProtective : EntityDto<Guid>
    {
        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual Guid? OccHazardFactorsId { get; set; }

        ///// <summary>
        ///// 父级单位
        ///// </summary>
        //public virtual string OccHazardFactors { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        [StringLength(500)]
        public virtual string Category { get; set; }
    }
}
