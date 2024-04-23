using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
    public class fullOccupationHistoryDto
    {
        /// <summary>
        /// 危机因素
        /// </summary>
        public List<OccdieaseHurtDto> OccHisHazardFactors { get; set; }


        /// <summary>
        /// 防护措施
        /// </summary>
        public List<OccdieaseProtectDto> OccHisProtectives { get; set; }
        /// <summary>
        /// 职业史
        /// </summary>

        public OccupationHistoryDto occupationHistoryDto { get; set; }
    }


}