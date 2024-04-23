using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto
{
    public class ViewInvalidInvoiceListDto
    {
        /// <summary>
        /// 发票号
        /// </summary>
        [StringLength(64)]
        public virtual string InvoiceNum { get; set; }

        /// <summary>
        /// 发票金额
        /// </summary>
        public virtual decimal InvoiceMoney { get; set; }
    }
}