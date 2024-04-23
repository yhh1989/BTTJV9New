using System.Collections.Generic;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto
{
    public class ViewDailyReportDto
    {
        /// <summary>
        /// 最小发票号
        /// </summary>
        public virtual string MinInvoiceNum { get; set; }
        /// <summary>
        /// 最大发票号
        /// </summary>
        public virtual string MaxInvoiceNum { get; set; }
        /// <summary>
        /// 团体体检发票张数
        /// </summary>
        public virtual int? GroupInvoiceCount { get; set; }

        /// <summary>
        /// 个人体检发票张数
        /// </summary>
        public virtual int? IndividualInvoiceCount { get; set; }

        /// <summary>
        /// 团体体检发票张数
        /// </summary>
        public virtual decimal? GroupInvoiceMoney { get; set; }

        /// <summary>
        /// 个人体检发票张数
        /// </summary>
        public virtual decimal? IndividualInvoiceMoney { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        public virtual decimal? ActualMoney { get; set; }
        /// <summary>
        /// 收费方式
        /// </summary>
        public virtual List<ViewChargeTypeListDto> ChargeTypes {get;set;}
        /// <summary>
        /// 作废发票
        /// </summary>
        public virtual List<ViewInvalidInvoiceListDto> InvalidInvoices { get; set; }
    }
}