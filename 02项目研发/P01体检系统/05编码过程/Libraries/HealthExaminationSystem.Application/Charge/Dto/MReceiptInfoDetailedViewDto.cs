using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{ 
#if !Proxy
    [AutoMapFrom(typeof(TjlMReceiptDetails))]
#endif
    public class MReceiptInfoDetailedViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 结算ID
        /// </summary>       
        public virtual Guid MReceiptInfoID { get; set; }

        /// <summary>
        /// 组合ID
        /// </summary>       
        public virtual Guid ItemGroupId { get; set; }       

        /// <summary>
        /// 组合编码
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupCodeBM { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }

        /// <summary>
        /// 收费类别序号
        /// </summary>
        public virtual int? SfTypeOrder { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>    
        public virtual Guid DepartmentId { get; set; }      
        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentCodeBM { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        public virtual int? IsAddMinus { get; set; }

        /// <summary>
        /// 收费状态 1未收费2个人已支付3单位已支付4.混合付款5赠检 只标示个人是否付费，团体
        /// </summary>
        public virtual int? PayerCat { get; set; }

        /// <summary>
        /// 收费分类
        /// </summary>
        [StringLength(64)]
        public virtual string ReceiptTypeName { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal GroupsMoney { get; set; }

        /// <summary>
        /// 折扣后
        /// </summary>
        public virtual decimal GroupsDiscountMoney { get; set; }

        /// <summary>
        /// 平均折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

    }
}
