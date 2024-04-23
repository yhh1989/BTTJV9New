using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMap(typeof(PersonnelCategory))]
#endif
    public class PersonnelCategoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 是否免费
        /// </summary>
        public bool IsFree { get; set; }

    }
}
