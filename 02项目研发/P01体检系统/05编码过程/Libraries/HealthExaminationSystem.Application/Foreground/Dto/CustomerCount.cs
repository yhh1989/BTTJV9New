using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Foreground.Dto
{
  public   class CustomerCount
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
     
        public virtual long? UserName { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        [StringLength(32)]
        public virtual int? RSCount { get; set; }

        /// <summary>
        /// 管数
        /// </summary>
        [StringLength(32)]
        public virtual int? BloodBarCount { get; set; }



    }
}
