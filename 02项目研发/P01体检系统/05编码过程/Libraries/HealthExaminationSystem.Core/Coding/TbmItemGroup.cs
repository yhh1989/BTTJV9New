using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Illness;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 项目组合设置
    /// </summary>
    public class TbmItemGroup : FullAuditedEntity<Guid>, IMustHaveTenant, IPassivable
    {
        /// <summary>
        /// 科室标识
        /// </summary>
        [ForeignKey(nameof(Department))]
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public virtual TbmDepartment Department { get; set; }

        /// <summary>
        /// 项目套餐
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual ICollection<TbmItemSuit> ItemSuits { get; set; }

        /// <summary>
        /// 项目信息
        /// </summary>
        [InverseProperty(nameof(TbmItemInfo.ItemGroups))]
        public virtual ICollection<TbmItemInfo> ItemInfos { get; set; }


        /// <summary>
        /// 医嘱项目信息
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual ICollection<TbmPriceSynchronize> TbmPriceSynchronizes { get; set; }

        /// <summary>
        /// 医嘱项目信息
        /// </summary>   
        public virtual ICollection<TbmGroupRePriceSynchronizes> GroupRePriceSynchronizes { get; set; }

        /// <summary>
        /// 使用的耗材
        /// </summary>
        public virtual ICollection<TbmConsumables> TbmConsumableses { get; set; }


        /// <summary>
        /// 复查项目
        /// </summary>
        public virtual ICollection<TjlCusReview> TjlCusReviews { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string ItemGroupBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// HIS组合ID
        /// </summary>
        [StringLength(64)]
        public virtual string HISID { get; set; }

        /// <summary>
        /// HIS组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string HISName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(64)]
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 性别 1男2女3不限制
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
        /// 单价 最小折扣核算后的价格
        /// </summary>
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        public virtual decimal? CostPrice { get; set; }

        /// <summary>
        /// 收费类别编码
        /// </summary>
        [StringLength(64)]
        public virtual string ChartCode { get; set; }

        /// <summary>
        /// 收费类别名称
        /// </summary>
        [StringLength(64)]
        public virtual string ChartName { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        [StringLength(256)]
        public virtual string Notice { get; set; }

        /// <summary>
        /// 组合介绍
        /// </summary>
        [StringLength(256)]
        public virtual string ItemGroupExplain { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 是否属于收费项目 1收费2不收费 默认 收费
        /// </summary>
        public virtual int? ISSFItemGroup { get; set; }

        /// <summary>
        /// 标本类型 字典
        /// </summary>
        public virtual int? SpecimenType { get; set; }

        /// <summary>
        /// 是否用打条码  1是2不是
        /// </summary>
        public virtual int? BarState { get; set; }

        /// <summary>
        /// 是否隐私项目 1是2不是
        /// </summary>
        public virtual int? PrivacyState { get; set; }

        /// <summary>
        /// 是否抽血 1是2不是
        /// </summary>
        public virtual int? DrawState { get; set; }

        /// <summary>
        /// 餐前餐后 1餐前2餐后
        /// </summary>
        public virtual int? MealState { get; set; }

        /// <summary>
        /// 是否妇检 1是2不是
        /// </summary>
        public virtual int? WomenState { get; set; }

        /// <summary>
        /// 禁忌备注
        /// </summary>
        [StringLength(256)]
        public virtual string Taboo { get; set; }

        /// <summary>
        /// 试管样式 字典
        /// </summary>
        [StringLength(64)]
        public virtual string TubeType { get; set; }

        /// <summary>
        /// 是否早餐 1不是2是
        /// </summary>
        public virtual int? Breakfast { get; set; }

        /// <summary>f
        /// 最大体检人数
        /// </summary>
        public virtual int? MaxCheckDay { get; set; }

        /// <summary>
        /// 项目明细数量
        /// </summary>
        public virtual int? ItemCount { get; set; }

        /// <summary>
        /// 自动VIP项目  1自动vip 2不自动 如果选该项目自动成为VIP
        /// </summary>
        public virtual int? AutoVIP { get; set; }

        /// <summary>
        /// 最大折扣
        /// </summary>
        public virtual decimal? MaxDiscount { get; set; }

        /// <summary>
        /// 外送状态 1外送 2自己做
        /// </summary>
        public virtual int? OutgoingState { get; set; }

        /// <summary>
        /// 是否询问自愿（如妇科，乙肝）  1是 2否
        /// </summary>
        public virtual int? VoluntaryState { get; set; }

        /// <summary>
        /// 项目组合排期信息
        /// </summary>
        [InverseProperty(nameof(TjlScheduling.ItemGroups))]
        public virtual ICollection<TjlScheduling> Schedulings { get; set; }
        /// <summary>
        /// 组合对应
        /// </summary> 
        [InverseProperty(nameof(TdbInterfaceItemGroupComparison.ItemGroup))]
        public virtual ICollection<TdbInterfaceItemGroupComparison> InterfaceItemGroupComparison { get; set; }

        /// <summary>
        /// 复查设置
        /// </summary> 
        [InverseProperty(nameof(TbmReviewItemSet.ItemGroupBM))]
        public virtual ICollection<TbmReviewItemSet> ReviewItemSet { get; set; }

        /// <summary>
        /// 作为必填项目时使用的职业健康列表
        /// </summary>
        [InverseProperty(nameof(OccupationalDiseaseIncludeItemGroup.MustHaveItemGroups))]
        public virtual ICollection<OccupationalDiseaseIncludeItemGroup> MustHaveOccupationalDiseaseIncludeItemGroups
        {
            get;
            set;
        }

        /// <summary>
        /// 作为选填项目时使用的职业健康列表
        /// </summary>
        [InverseProperty(nameof(OccupationalDiseaseIncludeItemGroup.MayHaveItemGroups))]
        public virtual ICollection<OccupationalDiseaseIncludeItemGroup> MayHaveOccupationalDiseaseIncludeItemGroups
        {
            get;
            set;
        }
        // <summary>
        /// 目标疾病
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual ICollection<TbmOccTargetDisease> OccTargetDisease { get; set; }


        /// <summary>
        /// 目标疾病必选项目
        /// </summary> 
        [InverseProperty(nameof(TbmOccTargetDisease.MustIemGroups))]
        public virtual ICollection<TbmOccTargetDisease> OccTargetDiseaseMustIemGroups
        {
            get;
            set;
        }

        /// <summary>
        /// 目标疾病可选项目
        /// </summary>     
        [InverseProperty(nameof(TbmOccTargetDisease.MayIemGroups))]
        public virtual ICollection<TbmOccTargetDisease> OccTargetDiseaseMayIemGroups
        {
            get;
            set;
        }
        /// <summary>
        /// 耗材关联项目
        /// </summary>       
        public virtual ICollection<TbmGroupConsumables> ConsumablesreIemGroups
        {
            get;
            set;
        }
       
        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 是否核酸检测
        /// </summary>
        public virtual int? NucleicAcidState { get; set; }

        /// <summary>
        /// 公卫编码
        /// </summary>     
        [MaxLength(64)]
        public virtual string GWBM { get; set; }

        /// <summary>
        /// 通知单编码
        /// </summary>     
        [MaxLength(64)]
        public virtual string NoticeCode { get; set; }

        /// <summary>
        /// 通知单名称
        /// </summary>     
        [MaxLength(64)]
        public virtual string NoticeName { get; set; }
        /// <summary>
        /// 检查类型编码
        /// </summary>     
        [MaxLength(64)]
        public virtual string InspectionTypeCode { get; set; }

        /// <summary>
        /// 检查类型编码名称
        /// </summary>     
        [MaxLength(64)]
        public virtual string InspectionTypeName { get; set; }


        /// <summary>
        /// 检查检验类型
        /// </summary>     

        [MaxLength(64)]
        public virtual string CheckTypeCode { get; set; }

        /// <summary>
        /// 检查检验类型编码名称
        /// </summary>       
        [MaxLength(64)]
        public virtual string CheckTypeName { get; set; }


        /// <summary>
        /// 价格更新 状态1未更新2已更新3已确认
        /// </summary>
        public virtual int? PriceState { get; set; }

        /// <summary>
        /// 价格更新时间
        /// </summary>
        public virtual DateTime? PriceTime { get; set; }
        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

    }
}