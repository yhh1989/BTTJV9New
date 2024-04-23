using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{
    /// <summary>
    /// 单位分组信息
    /// </summary>

    public class ClientTeamInfoInput
    {
        /// <summary>
        /// 预约Id
        /// </summary>
        public virtual Guid? ClientRegId { get; set; }

    }
}
