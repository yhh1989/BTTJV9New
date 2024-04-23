using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 收费记录表
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlMReceiptInfo))]
#endif
    public class MReceiptInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 是否成功 1成功 0异常
        /// </summary>      
#if !Proxy
        [IgnoreMap]
#endif
        public virtual string code { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>      
#if !Proxy
        [IgnoreMap]
#endif
        public virtual string err { get; set; }
#if !Proxy
        [IgnoreMap]
#endif
        public virtual string FormatMPayment
        {
            get
            {
                if (MPayment == null)
                    return string.Empty;
                var payInfos = MPayment.Aggregate(string.Empty, (current, payMent) => $"{current}{payMent.MChargeTypename}:{payMent.Actualmoney},");

                return payInfos.TrimEnd(',');
            }
        }

#if !Proxy
        [IgnoreMap]
#endif
        public virtual string FormatMInvoiceRecord
        {
            get 
            {
                string PayInfos = "";
                if (MInvoiceRecord != null)
                {
                    foreach (CreateInvoiceRecordDto InvoiceRecord in MInvoiceRecord)
                    {
                        PayInfos += InvoiceRecord.InvoiceNum + ",";
                    }
                }
                return PayInfos.TrimEnd(',');
            }
        }
        /// <summary>
        /// 个人预约ID
        /// </summary>
        public virtual CusRegSimpleDto CustomerReg { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual ClientRegDto ClientReg { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual ICollection<CreatePaymentDto> MPayment { get; set; }

        /// <summary>
        /// 发票记录
        /// </summary>
        public virtual ICollection<CreateInvoiceRecordDto> MInvoiceRecord { get; set; }


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
        public virtual UserForComboDto User { get; set; }

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

        ///// <summary>
        ///// 日结表编码
        ///// </summary>
        //  public virtual string DiurnalTableBM { get; set; }
        // public virtual TjlMDiurnalTableDto MDiurnalTable { get; set; }

    }
}