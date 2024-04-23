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

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 危害因素职业健康
    /// </summary>
    /// /// <summary>
    /// 体检人预约登记信息表
    /// </summary>
#if !Proxy 
    [AutoMapFrom(typeof(TbmOccTargetDisease))]
#endif
    public class OccTargetDiseaseByRisk
    {
        /// <summary>
        /// 处理意见
        /// </summary>
        [StringLength(500)]
        public virtual string Opinions { get; set; }

        /// <summary>
        /// 职业病
        /// </summary>
        public virtual List<OccCusDiseaseDto> OccDiseases { get; set; }

        /// <summary>
        /// 禁忌证
        /// </summary>
        public virtual List<OccCusContraindicationDto> Contraindications { get; set; }


        /// <summary>
    }
}
