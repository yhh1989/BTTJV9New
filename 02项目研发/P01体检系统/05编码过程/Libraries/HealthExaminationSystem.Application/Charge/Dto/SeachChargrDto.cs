using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 获取折扣、金额方法参数
    /// </summary>
    public class SeachChargrDto
    {
        /// <summary>
        /// 选择组合集合
        /// </summary>
        public ICollection<GroupMoneyDto> ItemGroups { get; set; }
        /// <summary>
        /// 操作用户
        /// </summary>
        public UserViewDto user { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayMoney { get; set; }
        /// <summary>
        /// 递归用到的 传0
        /// </summary>
        public decimal minMoney { get; set; }
        /// <summary>
        /// 递归用到的 传0
        /// </summary>
        public decimal minDiscountMoney { get; set; }
        /// <summary>
        /// 递归用到的 传0
        /// </summary>
        public decimal ZKL { get; set; }
    }
}