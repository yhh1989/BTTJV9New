using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
  public  class PushMessageDto
    {
  
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }


        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 收费时间
        /// </summary>
        public virtual DateTime? ChargeDate { get; set; }

        /// <summary>
        /// pushtype推送类别，4登记推送，5收费推送，6查报告推送7
        /// </summary>
        public int pushtype { get; set; }

        [StringLength(500)]
        public virtual string Message { get; set; }

    }
}
