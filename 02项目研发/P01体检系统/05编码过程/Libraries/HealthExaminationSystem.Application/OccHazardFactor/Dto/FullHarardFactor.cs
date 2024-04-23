using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto
{
   public class FullHarardFactor
    {
        /// <summary>
        /// 一条
        /// </summary>
        public  CreateOrUpdateHazardFactorDto HazardFactorDto { get; set; }

        public  List<HazardFactorsProtective> HazardFactorsProtectiveDto { get; set; }
    }
}
