using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class InCusBMDto
    {
        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerRegBM { get; set; }
        //开始时间
        public virtual DateTime? StartTime { get; set; }
        //结束时间
        public virtual DateTime? EndTime { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        public virtual string hospitalno { get; set; }
    }
}
