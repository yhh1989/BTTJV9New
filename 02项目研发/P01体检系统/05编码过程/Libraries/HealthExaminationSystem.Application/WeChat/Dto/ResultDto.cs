using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class ResultDto
    {
        /// <summary>
        /// 编码1成功0失败
        /// </summary>
        public virtual int? code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public virtual string Mess { get; set; }
    }
}
