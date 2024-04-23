using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    public class InputOccCusSumDto
    {
        /// <summary>
        /// 职业健康总检
        /// </summary>
        public virtual CreatOccCustomerSumDto CreatOccCustomerSumDto { get;set;}
        /// <summary>
        /// 职业健康总检-职业病
        /// </summary>
        public virtual List<CraetOccCustomerOccDiseasesDto> CraetOccCustomerOccDiseasesDto { get; set; }
        /// <summary>
        /// 职业健康总检-禁忌证
        /// </summary>
        public virtual List<CreatOccCustomerContraindicationDto> CreatOccCustomerContraindicationDto { get; set; }

        /// <summary>
        /// 危害因素总检
        /// </summary>
        public virtual List<OccCustomerHazardSumDto> OccCustomerHazardSumDto { get; set; }

    }
}
