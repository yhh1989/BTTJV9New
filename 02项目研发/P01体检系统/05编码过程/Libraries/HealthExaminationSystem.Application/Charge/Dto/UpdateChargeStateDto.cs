using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    public class UpdateChargeStateDto
    {
        /// <summary>
        /// 预约ID集合
        /// </summary>
        public virtual List<Guid> CusRegids { get; set; }
        /// <summary>
        /// 预约组合集合
        /// </summary>
        public virtual List<Guid> CusGroupids { get; set; }
        /// <summary>
        /// 结算ID
        /// </summary>
        public virtual Guid ReceiptID { get; set; }
        /// <summary>
        /// 个人或单位 1个人2单位
        /// </summary>
        public virtual int CusType { get; set; }

    }
}
