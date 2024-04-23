using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 用户预约组合
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class ChargeGroupsDto : EntityDto<Guid>
    {

#if !Proxy
        [IgnoreMap]
#endif
        public virtual string ChargeState
        {
            get
            {
                if (MReceiptInfoPersonalId.HasValue)
                {
                    
                    return "已收费";
                }
                else if (MReceiptInfoPersonalId.HasValue)
                {
                    return "已收费";
                }
                else
                {
                    return "未收费";
                }
            }
        }

        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual Guid? CustomerRegBMId { get; set; }

        ///// <summary>
        ///// 科室ID
        ///// </summary>
        //public virtual TbmDepartmentDto DepartmentBM { get; set; }

             

        /// <summary>
        /// 个人结账ID
        /// </summary>  
        public virtual Guid? MReceiptInfoPersonalId { get; set; }

        /// <summary>
        /// 单位结账ID
        /// </summary>      
        public virtual Guid? MReceiptInfoClientlId { get; set; }
        /// <summary>
        /// 科室外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>      
        public virtual Guid DepartmentId { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentName { get; set; }

        /// <summary>
        /// 科室排序
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

        ///// <summary>
        ///// 项目组合ID
        ///// </summary>
        // public virtual TbmItemGroupDto ItemGroupBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }

        /// <summary>
        /// 收费类别
        /// </summary>
        public virtual int? SfTypeOrder { get; set; }

        /// <summary>
        /// 收费类别 SFType字典
        /// </summary>
        public virtual int? SFType { get; set; }

        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查 6暂存
        /// </summary>
        public virtual int? CheckState { get; set; }


        public virtual int? IsAddMinus { get; set; }

        /// <summary>
        /// 退费状态 1正常2带退费3退费 收费处退费后变为减项状态
        /// </summary>
        public virtual int? RefundState { get; set; }

        /// <summary>
        /// 组合价格
        /// </summary>
        public virtual decimal ItemPrice { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal DiscountRate { get; set; }

        /// <summary>
        /// 折后价格
        /// </summary>
        public virtual decimal PriceAfterDis { get; set; }

        /// <summary>
        /// 收费状态 1未收费2个人已支付3单位已支付4.混合付款5赠检
        /// </summary>
        public virtual int? PayerCat { get; set; }

        /// <summary>
        /// 团体支付金额
        /// </summary>
        public virtual decimal TTmoney { get; set; }

        /// <summary>
        /// 个人支付金额
        /// </summary>
        public virtual decimal GRmoney { get; set; }

        /// <summary>
        /// 退费方式
        /// </summary>
        public virtual Guid? PaymentId { get; set; }

    }
}