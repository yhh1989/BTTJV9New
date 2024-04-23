using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.HisInterface
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(HealthExamination.Drivers.Models.HisInterface.InCarNumPrice))]
#endif
    public class InCarNumPriceDto
    {
        /// <summary>
        /// 诊疗卡号
        /// </summary>
        public string CardNum { get; set; }

        /// <summary>
        /// His厂家
        /// </summary>
        public string HISName { get; set; }
    }
}
