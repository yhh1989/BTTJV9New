using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemSuitItemGroupContrast))]
#endif
    public class FullItemGroup : EntityDto<Guid>
    {
        /// <summary>
        /// 组合Id
        /// </summary>
        public virtual Guid ItemGroupId { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 检查类型
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// 选择类别
        /// </summary>
        public virtual string SelectType { get; set; }
    }
}
