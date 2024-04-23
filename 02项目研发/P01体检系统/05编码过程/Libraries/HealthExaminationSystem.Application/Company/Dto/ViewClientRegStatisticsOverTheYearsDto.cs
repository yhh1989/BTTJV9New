using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    public class ViewClientRegStatisticsOverTheYearsDto
    {
        public TjlCustomerRegDto CustomerReg { get; set; }

        public int Year { get; set; }

        public long RegPersonCount { get; set; }
    }
}
