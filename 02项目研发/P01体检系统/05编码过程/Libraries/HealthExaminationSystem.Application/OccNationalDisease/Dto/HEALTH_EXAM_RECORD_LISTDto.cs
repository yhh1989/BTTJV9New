using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
    /// <summary>
    /// 职业健康档案 
    /// </summary>
    public class HEALTH_EXAM_RECORD_LISTDto
    {
        /// <summary>
        /// 职业健康档案
        /// </summary>
        public List<HEALTH_EXAM_RECORDDto> HEALTH_EXAM_RECORD { get; set; }
    }
}
