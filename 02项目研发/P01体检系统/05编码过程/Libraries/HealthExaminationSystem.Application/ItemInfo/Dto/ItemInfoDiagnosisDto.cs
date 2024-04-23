using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemInfo))]
#endif
    public class ItemInfoDiagnosisDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 项目打印名称
        /// </summary>
        public virtual string NamePM { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 项目类别 1数值型2计算型3说明型4阴阳性
        /// </summary>
        public virtual int? moneyType { get; set; }

        /// <summary>
        /// 项目标准值:用于文本型项目
        /// </summary>
        public virtual string ItemStandard { get; set; }

        /// <summary>
        /// 仪器Id:用于文本型项目
        /// </summary>
        public virtual string InstrumentId { get; set; }

        /// <summary>
        /// 项目下限:用于数值型项目
        /// </summary>
        public virtual decimal? minValue { get; set; }

        /// <summary>
        /// 项目上限:用于数值型项目
        /// </summary>
        public virtual decimal? maxValue { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string ItemBM { get; set; }
        /// <summary>
        /// 彩超和影像类 1所见、2诊断、3正常
        /// </summary>
        public virtual int? SeeState { get; set; }
    }
}