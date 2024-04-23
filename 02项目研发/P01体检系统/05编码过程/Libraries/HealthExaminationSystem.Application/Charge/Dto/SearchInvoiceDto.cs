using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    public  class SearchInvoiceDto
    {
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 查询名称
        /// </summary>
        [StringLength(128)]
        public string SearchName { get; set; }       
        /// <summary>
        /// 是否本人 1本人 2其他
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 收费作废
        /// </summary>
        public int ReceiptSate { get; set; }

        /// <summary>
        /// 单位预约ID
        /// </summary>
        public Guid? ClientRegId { get; set; }

        /// <summary>
        /// 收费员
        /// </summary>
        public long? SFUserId { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public Guid? ChargeType { get; set; }


    }
}
