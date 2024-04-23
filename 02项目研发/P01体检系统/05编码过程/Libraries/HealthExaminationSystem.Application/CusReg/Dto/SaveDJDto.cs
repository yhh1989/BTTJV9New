using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public  class SaveDJDto
    {

        /// <summary>
        /// 人员档案
        /// </summary>
        public virtual SaveCusInfoDto Customer { get; set; }
        /// <summary>
        /// 预约
        /// </summary>
        public virtual SaveCusRegInfoDto CustomerReg { get; set; }
        /// <summary>
        /// 组合
        /// </summary>
        public virtual List<SaveCusGroupsDto> CusGroups { get; set; }
        /// <summary>
        /// 危害因素 
        /// </summary>
        public virtual List<ShowOccHazardFactorDto> OccHazardFactors { get; set; }


    }
}
