using Abp.Application.Services.Dto;
using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 套餐统计
    /// </summary>
    public class SearchItemSuitStatisticsDto : EntityDto<Guid>
    {
        /// <summary>
        /// 套餐类别
        /// </summary>
        public virtual int? ItemSuitType { get; set; }

        public string ItemSuitTypeDisplay
        {
            get
            {
                switch (ItemSuitType)
                {
                    case 1:
                        return "套餐";
                    case 2:
                        return "组单";
                    default:
                        return "套餐";
                }
            }
        }
        /// <summary>
        /// 套餐id
        /// </summary>
        public virtual Guid? ItemSuitBMId { get; set; }
        /// <summary>
        /// 套餐名称
        /// </summary>
        public virtual string ItemSuitName { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public virtual decimal? CostPrice { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public virtual decimal? Price { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal? Discount { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        public virtual decimal Shouldmoney { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        public virtual decimal Actualmoney { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public virtual int Count { get; set; }
    }
}
