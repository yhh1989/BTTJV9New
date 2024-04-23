using System;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Dictionary.Dto
{
#if Application
    [AutoMap(typeof(TbmItemDictionary))]
#endif
    public class DictionaryItemDictionaryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 字典内容
        /// </summary>
        public virtual string Word { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 科室ID外键
        /// </summary>
        public virtual Guid? DepartmentBMId { get; set; }

        /// <summary>
        /// 项目ID外键
        /// </summary>
        public virtual Guid? iteminfoBMId { get; set; }

        /// <summary>
        /// 是否疾病 1重大疾病2一般疾病3阳性发现
        /// </summary>
        public virtual int? IsSickness { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 是否常用 1常用2正常
        /// </summary>
        public virtual int? ApplySate { get; set; }

        /// <summary>
        /// 疾病名称
        /// </summary>
        public virtual string Period { get; set; }


        /// <summary>
        /// 公卫编码
        /// </summary>       
        public virtual string GWBM { get; set; }
    }

}