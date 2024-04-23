using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
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
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccTargetDisease.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{ 
    /// <summary>
    /// 职业健康危害因素总检-职业健康
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlOccCustomerHazardSum))]
#endif
    public class OccCustomerHazardSumDto : EntityDto<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid? OccCustomerSummarizeId { get; set; }      

        /// <summary>
        /// 体检人预约主键
        /// </summary>  
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 危害因素Id
        /// </summary>
        public virtual Guid? OccHazardFactorsId { get; set; }

        public virtual ShowOccHazardFactorDto OccHazardFactors { get; set; }

        /// <summary>
        /// 职业病
        /// </summary>
        public virtual string OccCustomerOccDiseasesIds { get; set; }


        /// <summary>
        /// 职业病
        /// </summary>
        public virtual List<TbmOccDiseaseDto> OccCustomerOccDiseases { get; set; }

        /// <summary>
        /// 职业禁忌证
        /// </summary>
        public virtual string OccDictionarysIDs { get; set; }
        /// <summary>
        /// 禁忌证
        /// </summary>
        public virtual List<ShowOccDictionary> OccDictionarys { get; set; }

        
        /// <summary>
        /// 参考处理意见
        /// </summary>
        [StringLength(500)]
        public virtual string Opinions { get; set; }

        /// <summary>
        /// 职业健康结论
        /// </summary>
        [StringLength(5000)]
        public virtual string Conclusion { get; set; }


        /// <summary>
        /// 职业健康处理意见
        /// </summary>
        [StringLength(1000)]
        public virtual string Advise { get; set; }


        /// <summary>
        /// 职业健康结论描述
        /// </summary>
        [StringLength(1000)]
        public virtual string Description { get; set; }



        /// <summary>
        /// 医学建议
        /// </summary>
        [StringLength(1000)]
        public virtual string MedicalAdvice { get; set; }


        /// <summary>
        /// 是否主要
        /// </summary>
        public virtual int IsMain { get; set; }

    }
}
