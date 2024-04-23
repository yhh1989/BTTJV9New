using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
    /// <summary>
    /// 查询单位分组Dto
    /// </summary>
    public class QueryClientTeamDto
    {
        public Guid ClientRegId { get; set; }
        public string ClientName { get; set; }
        public Guid TeamNameId { get; set; }
        public string TeamName { get; set; }
    }
}
