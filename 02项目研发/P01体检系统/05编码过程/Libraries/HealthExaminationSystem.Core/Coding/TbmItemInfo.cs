using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 项目
    /// </summary>
    public class TbmItemInfo : FullAuditedEntity<Guid>, IMustHaveTenant  
    {
        /// <summary>
        /// 科室标识
        /// </summary>
        [ForeignKey("Department")]
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 字典集合
        /// </summary>
        public virtual ICollection<TbmItemDictionary> ItemDictionarys { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public virtual TbmDepartment Department { get; set; }

        /// <summary>
        /// 组合集合
        /// </summary>
        [InverseProperty(nameof(TbmItemGroup.ItemInfos))]
        public virtual ICollection<TbmItemGroup> ItemGroups { get; set; }

        /// <summary>
        /// 参考值集合
        /// </summary>
        public virtual ICollection<TbmItemStandard> ItemStandards { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        public virtual ICollection<TbmSummarizeAdvice> SummarizeAdvice { get; set; }

        /// <summary>
        /// 互斥项目
        /// </summary>
        public virtual ICollection<TbmItemInfo> ItemInfos { get; set; }

        public virtual ICollection<TbmCriticalSet> CriticalSets { get; set; } 

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [MaxLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        [Required]
        [MaxLength(64)]
        public virtual string ItemBM { get; set; }

        /// <summary>
        /// 项目打印名称
        /// </summary>
        [Required]
        [MaxLength(64)]
        public virtual string NamePM { get; set; }

        /// <summary>
        /// 项目英文名称
        /// </summary>
        [MaxLength(64)]
        public virtual string NameEngAbr { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [MaxLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [MaxLength(64)]
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
        [MaxLength(32)]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        [MaxLength(256)]
        public virtual string Notice { get; set; }

        /// <summary>
        /// 项目说明
        /// </summary>
        [MaxLength(256)]
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
        /// 是否在报告打印 1 打印 2不打印
        /// </summary>
        public virtual int? Ckisrpot { get; set; }

        /// <summary>
        /// 报告代码
        /// </summary>
        [MaxLength(64)]
        public virtual string ReportCode { get; set; }

        /// <summary>
        /// HIS代码
        /// </summary>
        [MaxLength(64)]
        public virtual string HISCode { get; set; }

        /// <summary>
        /// 标准编码
        /// </summary>
        [MaxLength(64)]
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
        /// 彩超和影像类 1所见、2诊断、3正常
        /// </summary>
        public virtual int? SeeState { get; set; }

        /// <summary>
        /// 项目对应
        /// </summary>
        public virtual ICollection<TdbInterfaceItemComparison> InterfaceItemComparison { get; set; }

        /// <summary>
        /// 计算型关联公式
        /// </summary>      
        //[ForeignKey(nameof(ItemProcExpress))]
        [NotMapped]
        [Obsolete("暂停使用！", true)]
        public virtual Guid? ItemProcExpressID { get; set; }
        /// <summary>
        /// 计算型关联公式
        /// </summary>      
        [NotMapped]
        [Obsolete("暂停使用！", true)]
        public virtual TbmItemProcExpress ItemProcExpress { get; set; }
        /// <summary>
        /// 计算型关联公式
        /// </summary>
        public virtual ICollection<TbmItemProcExpress> ItemProcExpreslist { get; set; }

        /// <summary>
        /// 目标疾病
        /// </summary>
        public virtual ICollection<TbmOccTargetDisease> OccTargetDisease { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsActive { get; set; }

        /// <summary>
        /// 公卫编码
        /// </summary>     
        [MaxLength(64)]
        public virtual string GWBM { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string UnitBM { get; set; }


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