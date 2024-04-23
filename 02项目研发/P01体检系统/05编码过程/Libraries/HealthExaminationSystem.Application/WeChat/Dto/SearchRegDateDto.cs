using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class SearchRegDateDto
    {
        /// <summary>
        /// 体检号（用于对方存储失败后再次返回给接口）
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public virtual DateTime? RegTime { get; set; }
    }
}
