using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class WJMainDto
    {    /// <summary>
         /// 问卷ID
         /// </summary>
        public virtual string checkNo { get; set; }
          /// <summary>
         /// 问卷
         /// </summary>
        public virtual List<AnswerjsonDto> Answerjson { get; set; }

    }
}
