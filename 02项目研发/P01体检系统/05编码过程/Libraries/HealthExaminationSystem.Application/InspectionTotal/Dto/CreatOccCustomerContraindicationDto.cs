using Abp.Application.Services.Dto;
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
    /// 职业健康总检-职业健康
    /// </summary>
#if !Proxy 
    [AutoMap(typeof(TjlOccCustomerContraindication))]
#endif
   public class CreatOccCustomerContraindicationDto : EntityDto<Guid>
    {

        /// <summary>
        /// 体检人预约主键
        /// </summary>       
        public virtual Guid? CustomerRegBMId { get; set; }       

        /// <summary>
        /// 职业健康总检Id
        /// </summary>       
        public virtual Guid? OccCustomerSumId { get; set; }       

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(500)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3072)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
