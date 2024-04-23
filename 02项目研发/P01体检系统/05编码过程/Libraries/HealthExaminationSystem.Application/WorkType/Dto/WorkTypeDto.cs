using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.WorkType.Dto
{
#if !Proxy
    [AutoMap(typeof(Core.Illness.WorkType))]
#endif
    public class WorkTypeDto : EntityDto<Guid>
    {

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(32)]
        public virtual string Num { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual int? Category { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public virtual int? Order { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [StringLength(32)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(32)]
        public virtual string Content { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}