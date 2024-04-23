
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlClientReg))]
#endif
    public class GetClientVerSionDto
    {
        /// <summary>
        /// 数据行版本号
        /// </summary>      
        public byte[] RowVersion { get; set; }
    }
}
