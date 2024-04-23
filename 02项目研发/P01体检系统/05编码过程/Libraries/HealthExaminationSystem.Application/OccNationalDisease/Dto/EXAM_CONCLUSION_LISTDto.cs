using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccNationalDisease.Dto
{
   public  class EXAM_CONCLUSION_LISTDto
    {
        /// <summary>
        /// 体检结论信息
        /// </summary>
        public List<EXAM_CONCLUSIONDto>  EXAM_CONCLUSION { get; set; }
    }
}
