using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    public  class InCusQusTionDto
    {
        /// <summary>
        /// 问卷外部ID
        /// </summary>
        public virtual string OutQuestionID { get; set; }       
    }
}
