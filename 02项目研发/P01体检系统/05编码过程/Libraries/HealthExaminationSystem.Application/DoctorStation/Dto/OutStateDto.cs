using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
   public  class OutStateDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsOK { get; set; }

        /// <summary>
        /// 错误 提示
        /// </summary>
        public string ErrMess { get; set; }

    }
}
