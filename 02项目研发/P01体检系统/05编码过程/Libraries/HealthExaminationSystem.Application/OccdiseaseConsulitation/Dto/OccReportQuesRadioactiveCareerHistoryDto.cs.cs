using Abp.Application.Services.Dto;

using Sw.Hospital.HealthExaminationSystem.Application.OccHazardFactor.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlOccQuesRadioactiveCareerHistory))]
#endif
    public class OccReportQuesRadioactiveCareerHistoryDto
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>   
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StarTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public virtual DateTime? EndTime { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        [StringLength(100)]
        public virtual string WorkClient { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [StringLength(100)]
        public virtual string WorkName { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        [StringLength(100)]
        public virtual string WorkType { get; set; }

        /// <summary>
        /// 照射种类
        /// </summary>
        [StringLength(640)]
        public virtual string RadiationIds { get; set; }

        /// <summary>
        /// 照射种类
        /// </summary>
        public virtual ICollection<RadiationDto> Radiations { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatRadiations
        {
            get
            {
                if (Radiations != null && Radiations.Count > 0)
                {
                  return   string.Join(",", Radiations.Select(p=>p.Text).ToList());
                    ;
                }
                else
                {
                    return "";
                }
            }

        }

        /// <summary>
        /// 放射线种类ID
        /// </summary>
        [StringLength(640)]
        public virtual string TbmOccDictionaryIDs { get; set; }

        /// <summary>
        /// 放射线种类
        /// </summary>
        public virtual ICollection<ShowOccDictionary> TbmOccDictionarys { get; set; }
#if Application
        [IgnoreMap]
#endif
        public virtual string FormatDictionarys
        {
            get
            {
                if (TbmOccDictionarys != null && TbmOccDictionarys.Count > 0)
                {
                    return string.Join(",", TbmOccDictionarys.Select(p => p.Text).ToList());
                    ;
                }
                else
                {
                    return "";
                }
            }

        }
        /// <summary>
        /// 每日工作时或工作量
        /// </summary>    
        public virtual decimal? Workload { get; set; }

        /// <summary>
        /// 累计受照剂量
        /// </summary>    
        public virtual decimal? Cumulative { get; set; }

        /// <summary>
        /// 过量照射史
        /// </summary>
        [StringLength(640)]
        public virtual string Overdose { get; set; }
        /// <summary>
        /// 佩戴个人剂量计
        /// </summary>
        [StringLength(640)]
        public virtual string Dosimeter { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(640)]
        public virtual string Remarks { get; set; }
    }
}
