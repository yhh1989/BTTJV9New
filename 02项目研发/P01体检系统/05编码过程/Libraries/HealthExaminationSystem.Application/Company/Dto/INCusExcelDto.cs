using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
  public   class INCusExcelDto
    {
        /// <summary>
        /// 单位预约分组信息
        /// </summary>
        public List<CreateClientTeamInfoesDto> ListClientTeam { get; set; }
        /// <summary>
        /// 单位预约分组信息
        /// </summary>
        public DataTable CusList { get; set; }

    }
}
