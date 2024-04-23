using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlMReceiptInfo))]
#endif
    public class ReceiptInfoPerViewDto : EntityDto<Guid>
    { 

        //public virtual List<InvoiceRecordViewDto> MInvoiceRecord { get; set; }
        /// <summary>
        /// 个人预约ID
        /// </summary>
        public virtual Guid CustomerRegid { get; set; }

        //public virtual List<MReceiptInfoDetailedViewDto> MReceiptInfoDetailed { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual Guid ClientRegid { get; set; }

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
        /// 收费状态:1正常收费2退费
        /// </summary>
        public virtual int ReceiptSate { get; set; }

        /// <summary>
        /// 是否结算:1已结算2未结算
        /// </summary>
        public virtual int SettlementSate { get; set; }


        public virtual string xsdjbh { get; set; }
        /// <summary>
        /// 合计金额
        /// </summary>
        public virtual string hjje { get; set; }
        /// <summary>
        /// 合计税额
        /// </summary>
        public virtual string hjse { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public virtual string kprq { get; set; }

        /// <summary>
        /// 所属月份
        /// </summary>
        public virtual string ssyf { get; set; }

        /// <summary>
        /// 发票代码
        /// </summary>
        public virtual string fpdm { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        public virtual string fphm { get; set; }


        /// <summary>
        /// 清单标志
        /// </summary>
        public virtual string qdbz { get; set; }


        /// <summary>
        /// 发票密文
        /// </summary>
        public virtual string mw { get; set; }


        /// <summary>
        /// 校验码
        /// </summary>
        public virtual string jym { get; set; }


        /// <summary>
        /// 数字签名
        /// </summary>
        public virtual string qmz { get; set; }


        /// <summary>
        /// 二维码
        /// </summary>
        public virtual string ewm { get; set; }


        /// <summary>
        /// 机器编号
        /// </summary>
        public virtual string jqbh { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual ICollection<CreatePaymentDto> MPayment { get; set; }



    }
}