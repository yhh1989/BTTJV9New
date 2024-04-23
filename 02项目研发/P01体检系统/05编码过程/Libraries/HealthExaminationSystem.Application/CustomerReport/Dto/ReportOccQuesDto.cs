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
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlOccQuestionnaire))]
#endif
    public class ReportOccQuesDto
    {
              
        public virtual ReportOccQueAllDto ReportOccQueAll { get; set; }
        /// <summary>
        /// 职业史
        /// </summary>
        public virtual List<ReportQuesCareerHistoryDto> OccCareerHistory { get; set; }
        /// <summary>
        /// 放射职业史 
        /// </summary>
        public virtual List<OccReportQuesRadioactiveCareerHistoryDto> OccRadioactiveCareerHistory { get; set; }
        /// <summary>
        /// 既往史
        /// </summary>
        public virtual List<ReportOccQuesPastHistoryDto> OccPastHistory { get; set; }

        /// <summary>
        /// 家族史
        /// </summary>
        public virtual List<ReportOccQuesFamilyHistoryDto> OccFamilyHistory { get; set; }


        /// <summary>
        /// 症状
        /// </summary>
        public virtual List<ReportOccQuesSymNameDto> OccQuesSymptom { get; set; }

       /// <summary>
       /// 婚姻史
       /// </summary>
        public virtual List<OccReportQuesMerriyHistoryDto> OccQuesMerriyHistory { get; set; }



    }
}
