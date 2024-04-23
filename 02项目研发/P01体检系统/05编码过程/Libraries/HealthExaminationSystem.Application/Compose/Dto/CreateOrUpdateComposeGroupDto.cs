using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto
{
#if !Proxy
    [AutoMapTo(typeof(TbmComposeGroup))]
#endif
    public class CreateOrUpdateComposeGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int MaxAge { get; set; }

        /// <summary>
        /// 结婚状态
        /// </summary>
        public virtual int MaritalStatus { get; set; }

        /// <summary>
        /// 是否备孕 1备孕2不备孕
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

    }
}
