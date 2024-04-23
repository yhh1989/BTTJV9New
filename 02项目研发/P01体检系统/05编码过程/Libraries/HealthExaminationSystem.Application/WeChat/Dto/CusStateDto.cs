using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class CusStateDto
    {
        /// <summary>
        /// 医院编号
        /// </summary>
        public virtual string hospitalno { get; set; }
        /// <summary>
        /// 体检预约单号
        /// </summary>
        public virtual string orderId { get; set; }
        /// <summary>
        /// 是否已登记；1：已登记  0：未登记
        /// </summary>
        public virtual string isregiser { get; set; }
        /// <summary>
        /// 登记时间
        /// </summary>
        public virtual DateTime? regisertime { get; set; }
        /// <summary>
        /// 是否已完成体检；1：已完成  0：未完成
        /// </summary>
        public virtual int isfinishcheck { get; set; }
        /// <summary>
        /// 体检完成时间
        /// </summary>
        public virtual DateTime? finishchecktime { get; set; }
    }
}
