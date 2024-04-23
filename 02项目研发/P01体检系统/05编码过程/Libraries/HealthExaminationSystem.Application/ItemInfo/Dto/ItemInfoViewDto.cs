using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TbmItemInfo))]
#endif
    public class ItemInfoViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别 1男2女3不限
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>       
        [StringLength(64)]
        public virtual string ItemBM { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(32)]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        [StringLength(256)]
        public virtual string Notice { get; set; }

        /// <summary>
        /// 项目说明
        /// </summary>
        [StringLength(256)]
        public virtual string Remark { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public virtual DepartmentIdNameDto Department { get; set; }
        /// <summary>
        /// 是否删除（是否启用）
        /// </summary>
        public virtual int? IsDeleted { get; set; }
        /// <summary>
        /// 项目类别 1数值型2计算型3说明型4阴阳性
        /// </summary>
        public virtual int? moneyType { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsActive { get; set; }


        /// <summary>
        /// 标准编码
        /// </summary>    
        public virtual string StandardCode { get; set; }
    }
}