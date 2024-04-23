using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto
{
 public   class OccDieaseAndStandardDto
    {
        //一条职业健康信息
        public CreatOccDiseaseDto occDisease { get; set; }
        //多条职业健康标准
        public List<OccDiseaseStandardDto> listStandard { get; set; }
    }
}
