using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class ReceiptIdDto
    {
        /// <summary>
        /// 收费Id（退费用）
        /// </summary>

        public virtual Guid? ReceiptId { get; set; }
    }
}
