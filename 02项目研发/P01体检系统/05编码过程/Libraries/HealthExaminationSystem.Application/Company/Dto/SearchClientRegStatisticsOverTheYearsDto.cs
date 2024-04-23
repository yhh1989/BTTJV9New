using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    public class SearchClientRegStatisticsOverTheYearsDto
    {
        public Guid? ClientId { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
    }
}
