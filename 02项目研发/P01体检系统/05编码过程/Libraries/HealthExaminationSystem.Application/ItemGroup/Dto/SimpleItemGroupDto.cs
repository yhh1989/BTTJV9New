using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemGroup))]
#endif
    public class SimpleItemGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室Id
        /// </summary>
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public virtual DepartmentSimpleDto Department { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

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
        /// 成本价
        /// </summary>
        public virtual decimal? CostPrice { get; set; }

        /// <summary>
        /// 单价 最小折扣核算后的价格
        /// </summary>
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 最大折扣
        /// </summary>
        public virtual decimal? MaxDiscount { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 组合说明
        /// </summary>
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 组合介绍
        /// </summary>
        public virtual string ItemGroupExplain { get; set; }

        /// <summary>
        /// 收费类别编码
        /// </summary>
        [StringLength(64)]
        public virtual string ChartCode { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }

        /// <summary>
        /// 自动VIP项目  1自动vip 0不自动 如果选该项目自动成为VIP
        /// </summary>
        public virtual int? AutoVIP { get; set; }

        /// <summary>
        /// 是否妇检 1是2不是
        /// </summary>
        public virtual int? WomenState { get; set; }

        /// <summary>
        /// 性别 1男2女3不限制
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 核酸检测
        /// </summary>
        public virtual int? NucleicAcidState { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public  bool IsActive { get; set; }

        public virtual int? PrivacyState { get; set; }

        //        #region 格式化

        //        /// <summary>
        //        /// 性别格式化
        //        /// </summary>
        //#if Application
        //        [IgnoreMap]
        //#endif
        //        [JsonIgnore]
        //        public virtual string FormatSex => SexHelper.GetSexModelsForItemInfo().Where(o => o.Id == Sex).Select(o => o.Display).FirstOrDefault();

        //        #endregion
        //是否隐私项目 1是2不是



    }
}