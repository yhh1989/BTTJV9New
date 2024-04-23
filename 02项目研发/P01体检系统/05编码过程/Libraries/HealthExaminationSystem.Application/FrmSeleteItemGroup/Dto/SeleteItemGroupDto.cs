using System;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.FrmSeleteItemGroup.Dto
{
    /// <summary>
    /// 项目组合设置
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmItemGroup))]
#endif
    public class SeleteItemGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室
        /// </summary>
        public virtual SelectDepartmentDto Department { get; set; }

        /// <summary>
        /// 用户预约组合ID
        /// </summary>

        public virtual Guid CustomerItemGroupId { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 单价 最小折扣核算后的价格
        /// </summary>
        public virtual decimal Price { get; set; }

        /// <summary>
        /// 组合介绍
        /// </summary>
        public virtual string ItemGroupExplain { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal DiscountRate { get; set; }

        /// <summary>
        /// 折后价格
        /// </summary>
        public virtual decimal PriceAfterDis { get; set; }

        /// <summary>
        /// 团体支付金额
        /// </summary>
        public virtual decimal TTmoney { get; set; }

        /// <summary>
        /// 个人支付金额
        /// </summary>
        public virtual decimal GRmoney { get; set; }

        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项
        /// </summary>
        public virtual int? IsAddMinus { get; set; }

        /// <summary>
        /// 收费状态 1未收费2个人已支付3单位已支付
        /// </summary>
        public virtual int? PayerCat { get; set; }
        /// <summary>
        /// 收费类别编码
        /// </summary>
        public virtual string ChartCode { get; set; }
        /// <summary>
        /// 患者性别
        /// </summary>
        public virtual int? CurSex { get; set; }
        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 收费类别名称
        /// </summary>
        public virtual string ChartName { get; set; }
    }
}