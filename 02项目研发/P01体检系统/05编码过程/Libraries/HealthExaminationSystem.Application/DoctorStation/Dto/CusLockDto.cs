using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
  public  class CusLockDto
    {

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool? IsLock { get; set; }

        /// <summary>
        /// 锁定医生
        /// </summary>
        public string LockUser { get; set; }
    }
}
