using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
   public  class ReportOccQuesHisHazardDto
    {
        /// <summary>
        /// 危害因素名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
    }
}
