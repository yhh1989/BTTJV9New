using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlClientTeamInfo))]
#endif
    public class ClientTeamPhysicalDto
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public virtual int TeamBM { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }
    }
}
