using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;

namespace Sw.Hospital.HealthExaminationSystem.Core.Payment
{
    /// <summary>
    /// 结算表 抹零金额须通过应收-实收 
    /// </summary>
    public class TjlMReceiptInfo : FullAuditedEntity<Guid>, IMustHaveTenant, ICloneable
    {
        /// <summary>
        /// 作废原ID
        /// </summary>
        public virtual Guid? InvalidTjlMReceiptInfoId { get; set; }

        /// <summary>
        /// 发票记录
        /// </summary>
        public virtual ICollection<TjlMInvoiceRecord> MInvoiceRecord { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public virtual ICollection<TjlMPayment> MPayment { get; set; }

        /// <summary>
        /// 体检人预约Id外键
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 个人预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 体检人信息标识
        /// </summary>
        [ForeignKey(nameof(Customer))]
        public virtual Guid? CustomerId { get; set; }

        /// <summary>
        /// 体检人
        /// </summary>
        public virtual TjlCustomer Customer { get; set; }

        /// <summary>
        /// 项目明细
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual ICollection<TjlMReceiptInfoDetailed> MReceiptInfoDetailed { get; set; }

        /// <summary>
        /// 结算明细记录
        /// </summary>
        [InverseProperty(nameof(TjlMReceiptDetails.MReceiptInfo))]
        public virtual ICollection<TjlMReceiptDetails> MReceiptDetailses { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        [ForeignKey("ClientReg")]
        public virtual Guid? ClientRegId { get; set; }


        /// <summary>
        /// 申请单
        /// </summary>
        public virtual TjlApplicationForm ApplicationForm { get; set; }

        /// <summary>
        /// 申请单ID
        /// </summary>
        [ForeignKey("ApplicationForm")]
        public virtual Guid? ApplicationFormId { get; set; }
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
        /// 收费员标识
        /// </summary>
        [ForeignKey("User")]
        public virtual long? UserId { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 交费标示:3普通4储值5免费
        /// </summary>
        public virtual int ChargeState { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public virtual decimal Discontmoney { get; set; }

        /// <summary>
        /// 优惠原因
        /// </summary>
        [StringLength(256)]
        public virtual string DiscontReason { get; set; }

        /// <summary>
        /// 收费状态:1正常收费2作废3已作废
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
        /// 返回代码
        /// </summary>
        public virtual string returnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public virtual string returnMsg { get; set; }
        /// <summary>
        /// 请求流水号
        /// </summary>
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
        /// 商户号
        /// </summary>
        public string merchant_id { get; set; }
        /// <summary>
        /// 支付平台支付订单号
        /// </summary>
        public string pay_order_id { get; set; }

        /// <summary>
        /// 日结表编码
        /// </summary>

        //  public virtual string DiurnalTableBM { get; set; }
        public virtual TjlMDiurnalTable MDiurnalTable { get; set; }

        /// <summary>
        /// 克隆一个对象
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}