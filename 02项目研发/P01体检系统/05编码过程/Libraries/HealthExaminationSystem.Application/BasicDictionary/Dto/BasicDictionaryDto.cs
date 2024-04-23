using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.BasicDictionary.Dto
{

#if !Proxy
    [AutoMapFrom(typeof(TbmBasicDictionary))]
#endif
    public class BasicDictionaryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 值
        /// </summary>
        public virtual int Value { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        [StringLength(64)]
        public virtual string Text { get; set; }
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
        /// 字典类别，来自程序的枚举
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [StringLength(64)]
        public virtual string Code { get; set; }
    }
}
