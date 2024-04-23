#if Application
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
#if Application
    [Abp.AutoMapper.AutoMap(typeof(TjlClientInfo))]
#endif
    public class GroupClientNameDto
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 企业负责人
        /// </summary>
        public virtual string LinkMan { get; set; }
       
    }
}
