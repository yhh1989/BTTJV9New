using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public   class SeachRepot
    {
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        ///身份证号
        /// </summary>
        public virtual string IdCard { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? starTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }
        /// <summary>
        /// 异常列表
        /// </summary>
        public virtual List<string> ErrCustomerBMs { get; set; }
        

    }
}
