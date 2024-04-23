using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
    /// <summary>
    /// 单位团体预约
    /// </summary>
    public class ClientTeamRegDto
    {
        /// <summary>
        /// 单位预约信息
        /// </summary>
        public CreateClientRegDto ClientRegDto { get; set; }

        /// <summary>
        /// 单位预约分组信息
        /// </summary>
        public List<CreateClientTeamInfoesDto> ListClientTeam { get; set; }      

        

        /// <summary>
        /// 单位预约分组项目信息
        /// </summary>
        public List<ClientTeamRegitemViewDto> ListClientTeamItem { get; set; }




    }
}
