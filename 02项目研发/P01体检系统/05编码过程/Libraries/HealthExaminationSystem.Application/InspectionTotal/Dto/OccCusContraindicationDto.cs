#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif 
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto 
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmOccTargetDiseaseContraindication))]
#endif 
    public class OccCusContraindicationDto : EntityDto<Guid>
    {

        public virtual Guid? OccHazardFactorsId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
