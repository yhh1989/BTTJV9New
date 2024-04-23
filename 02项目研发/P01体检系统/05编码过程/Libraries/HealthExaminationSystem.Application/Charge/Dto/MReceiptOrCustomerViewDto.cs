using System;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlMReceiptInfo))]
#endif
    public class MReceiptOrCustomerViewDto : EntityDto<Guid>
    {
        //体检人、体检人预约
        public CustomerRegViewDto CustomerReg { get; set; }

        //public virtual List<InvoiceRecordViewDto> MInvoiceRecord { get; set; }

        //public virtual List<MReceiptInfoDetailedViewDto> MReceiptInfoDetailed { get; set; }

        ///// <summary>
        ///// 单位预约ID
        ///// </summary>
        //public virtual Guid ClientRegid { get; set; }

        ///// <summary>
        ///// 收费日期
        ///// </summary>
        //public virtual DateTime ChargeDate { get; set; }

        ///// <summary>
        ///// 总费用:原价放在第一笔消费，如果加项放在第二笔
        ///// </summary>
        //public virtual decimal Summoney { get; set; }

        ///// <summary>
        ///// 应收:优惠
        ///// </summary>
        //public virtual decimal Shouldmoney { get; set; }

        ///// <summary>
        ///// 实收
        ///// </summary>
        //public virtual decimal Actualmoney { get; set; }

        ///// <summary>
        ///// 折扣率
        ///// </summary>
        //public virtual decimal Discount { get; set; }

        ///// <summary>
        ///// 收费员
        ///// </summary>
        ////public virtual long Userid { get; set; }
        //public virtual UserSimpleViewDto Userid { get; set; }
    }
}