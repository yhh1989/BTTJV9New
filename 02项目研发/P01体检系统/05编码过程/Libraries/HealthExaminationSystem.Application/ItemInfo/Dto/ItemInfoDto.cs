using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemInfo))]
#endif
    public class ItemInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual Guid DepartmentId { get; set; }
        /// <summary>
        /// 科室
        /// </summary>

        public virtual TbmDepartmentDto Department { get; set; }

        /// <summary>
        /// 组合集合
        /// </summary>
        /// <remarks>
        /// 这个组合字段时给谁使用的，为什么在更新的不处理
        /// 每次保存导致组合关系丢失
        /// </remarks>
        public virtual List<ItemGroupDto> ItemGroups { get; set; }
        /// <summary>
        /// 互斥项目
        /// </summary>
        public virtual List<ItemInfoSimpleDto> ItemInfos { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 项目打印名称
        /// </summary>
        public virtual string NamePM { get; set; }

        /// <summary>
        /// 项目英文名称
        /// </summary>
        public virtual string NameEngAbr { get; set; }


        /// <summary>
        /// 助记码
        /// </summary>
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 性别 1男2女3不限
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual string Unit { get; set; }
        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string UnitBM { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        public virtual string Notice { get; set; }

        /// <summary>
        /// 项目说明
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 项目类别 1数值型2计算型3说明型4阴阳性
        /// </summary>
        public virtual int? moneyType { get; set; }

        /// <summary>
        /// 临床类别
        /// </summary>
        public virtual int? Lclxid { get; set; }

        /// <summary>
        /// 是否在报告打印 1否2打印
        /// </summary>
        public virtual int? Ckisrpot { get; set; }

        /// <summary>
        /// 报告代码
        /// </summary>
        public virtual string ReportCode { get; set; }

        /// <summary>
        /// HIS代码
        /// </summary>
        public virtual string HISCode { get; set; }

        /// <summary>
        /// 标准编码
        /// </summary>
        public virtual string StandardCode { get; set; }

        /// <summary>
        /// 是否复合判断 复合判断修改时更新此状态 1在2不在
        /// </summary>
        public virtual int? DiagnosisComplexSate { get; set; }

        /// <summary>
        /// 是否进入小结 1进入2不进入
        /// </summary>
        public virtual int? IsSummary { get; set; }

        /// <summary>
        /// 小结时不显示项目名称 1显示2不显示
        /// </summary>
        public virtual int? IsSummaryName { get; set; }

        /// <summary>
        /// 小数点 默认两位
        /// </summary>
        public virtual int? ItemDecimal { get; set; }

        /// <summary>
        /// 项目标准值:用于文本型项目
        /// </summary>
        public virtual string ItemStandard { get; set; }

        /// <summary>
        /// 仪器Id:用于文本型项目
        /// </summary>
        public virtual string InstrumentId { get; set; }

        ///// <summary>
        ///// 项目下限:用于数值型项目
        ///// </summary>
        //public virtual int? minValue { get; set; }

        ///// <summary>
        ///// 项目上限:用于数值型项目
        ///// </summary>
        //public virtual int? maxValue { get; set; }
        /// <summary>
        /// 彩超和影像类 1所见、2诊断、3正常
        /// </summary>
        public virtual int? SeeState { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string ItemBM { get; set; }

        /// <summary>
        /// 公卫编码
        /// </summary>     
        [StringLength(64)]
        public virtual string GWBM { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsActive { get; set; }


        /// <summary>
        /// 临界值最小值
        /// </summary>
        public virtual decimal? MinValue { get; set; }

        /// <summary>
        /// 临界值最大值
        /// </summary>
        public virtual decimal? MaxValue { get; set; }
        /// <summary>
        /// 是否启用临界值
        /// </summary>
        public virtual int? ISLJ { get; set; }
    }
}