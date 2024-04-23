using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    public class SearchBiseDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? starTime { get; set; }
        /// <summary>
        /// 异常列表
        /// </summary>
        public virtual List<string> ErrIDBMs { get; set; }
    }
}
