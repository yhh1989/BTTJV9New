using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public  class ErrList
    {
        public virtual string  customerBM { get; set; }

        public virtual DateTime? lastTime { get; set; }
    }
}
