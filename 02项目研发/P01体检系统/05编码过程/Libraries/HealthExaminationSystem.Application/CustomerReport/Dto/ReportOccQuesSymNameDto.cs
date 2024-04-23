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

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlOccQuesSymptom))]
#endif
    public class ReportOccQuesSymNameDto
    {
        /// <summary>
        ///症状名称
        /// </summary>
        [StringLength(500)]
        public virtual string Name { get; set; }
    }
}
