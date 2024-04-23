#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto
{
    /// <summary>
    /// 总检禁忌证
    /// </summary>
#if !Proxy
    [AutoMapFrom(typeof(TjlOccCustomerContraindication))]
#endif
    public class OccCustomerContraindicationDto
    {

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
    }
}
