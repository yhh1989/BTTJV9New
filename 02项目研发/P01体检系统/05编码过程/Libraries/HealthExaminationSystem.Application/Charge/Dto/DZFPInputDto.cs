using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
 public    class DZFPInputDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 收费ID
        /// </summary>
        public virtual Guid? TjlMReceiptId { get; set; }

        /// <summary>
        /// 收费时间
        /// </summary>
        public virtual DateTime? ChargeDate { get; set; }

        //收费人
        public virtual string UserName { get; set; }
        /// <summary>
        /// 实收金额
        /// </summary>
        public virtual decimal Actualmoney { get; set; }
    }
}
