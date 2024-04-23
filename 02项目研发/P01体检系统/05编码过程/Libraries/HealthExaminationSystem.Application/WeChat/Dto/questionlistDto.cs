using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public  class questionlistDto
    {
        /// <summary>
        /// 问题
        /// </summary>
        public virtual string questionname { get; set; }

        /// <summary>
        /// 答案，对于多选题，用顿号分割选项
        /// </summary>
        public virtual string answer { get; set; }

        /// <summary>
        /// 数组
        /// </summary>
        public virtual string[] answerlist { get; set; }


    }
}
