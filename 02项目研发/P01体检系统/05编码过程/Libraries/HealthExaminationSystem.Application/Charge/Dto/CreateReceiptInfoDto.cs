
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using AutoMapper;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{

    /// <summary>
    /// 结算表 抹零金额须通过应收-实收
    /// </summary>
#if !Proxy
    [AutoMapTo(typeof(TjlMReceiptInfo))]
#endif
    public class CreateReceiptInfoDto : EntityDto<Guid>
    {

        public virtual ICollection<CreateInvoiceRecordDto> MInvoiceRecordgr { get; set; }

        /// <summary>
        /// 个人预约ID
        /// </summary>
        // public virtual int CustomerRedID { get; set; }
        public virtual Guid? CustomerRegid { get; set; }
        
        public virtual ICollection<CreateMReceiptInfoDetailedDto> MReceiptInfoDetailedgr { get; set; }


        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual ICollection<CreatePaymentDto> MPaymentr { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        // public virtual string ClientRedID { get; set; }
        public virtual Guid? ClientRegid { get; set; }

        /// <summary>
        /// 体检类型:1单位2体检人
        /// </summary>
        public virtual int TJType { get; set; }

        /// <summary>
        /// 收费日期
        /// </summary>
        public virtual DateTime ChargeDate { get; set; }

        /// <summary>
        /// 总费用:原价放在第一笔消费，如果加项放在第二笔
        /// </summary>
        public virtual decimal Summoney { get; set; }

        /// <summary>
        /// 应收:优惠
        /// </summary>
        public virtual decimal Shouldmoney { get; set; }

        /// <summary>
        /// 实收
        /// </summary>
        public virtual decimal Actualmoney { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        //    public virtual int ChargeEmployeeID { get; set; }
        public virtual long Userid { get; set; }

        /// <summary>
        /// 交费标示:1正常收费2欠费3有退费4预支付
        /// </summary>
        public virtual int ChargeState { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public virtual decimal Discontmoney { get; set; }

        /// <summary>
        /// 优惠原因
        /// </summary>
        [StringLength(64)]
        public virtual string DiscontReason { get; set; }

        /// <summary>
        /// 收费状态:1正常收费2退费
        /// </summary>
        public virtual int ReceiptSate { get; set; }

        /// <summary>
        /// 是否结算:1已结算2未结算
        /// </summary>
        public virtual int SettlementSate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(128)]
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 支付平台支付订单号
        /// </summary>
        public string pay_order_id { get; set; }

        /// <summary>
        /// 用户付款码，付款码支付（被扫）必传
        /// </summary>
#if Application
        [IgnoreMap]
#endif
        public string auth_code { get; set; }

        /// <summary>
        /// 申请单ID
        /// </summary>
        public virtual Guid? ApplicationFormId { get; set; }

    }
}
