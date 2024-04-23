using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class AnswerjsonDto
    {
        /// <summary>
        /// 问题组标题
        /// </summary>
        public virtual string groupname { get; set; }
        /// <summary>
        /// 数组
        /// </summary>
        public virtual List< questionlistDto > questionlist { get; set; }
}
}
