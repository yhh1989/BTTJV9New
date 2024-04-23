using Abp.Application.Services.Dto;
#if !Proxy
using AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
    //职业史dto

    /// <summary>
    /// 查询客户预约信息Dto 
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlOccQuesCareerHistory))]
#endif
    public class OccupationHistoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 问卷Id
        /// </summary>
        public virtual Guid? OccQuestionnaireBMId { get; set; }
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

        public virtual string WorkClient { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        public virtual string WorkType { get; set; }

        /// <summary>
        /// 工龄
        /// </summary>
        public virtual decimal? WorkYears { get; set; }

        /// <summary>
        /// 工龄文本
        /// </summary>
        public virtual string StrWorkYears { get; set; }
        /// <summary>
        /// 工龄单位
        /// </summary>
        public virtual string UnitAge { get; set; }
        /// <summary>
        /// 车间
        /// </summary>

        public virtual string WorkName { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>       
        public virtual string HazardFactorIds { get; set; }
        /// <summary>
        /// 防护措施
        /// </summary>      
        public virtual string ProtectiveIds { get; set; }


        /// <summary>
        /// 危机因素
        /// </summary>
        public List<OccdieaseHurtDto> OccHisHazardFactors { get; set; }


        /// <summary>
        /// 防护措施
        /// </summary>
        public List<OccdieaseProtectDto> OccHisProtectives { get; set; }
#if Application
        [IgnoreMap]
#endif
        /// <summary>
        /// 防护措施
        /// </summary>
        public virtual string HisProtectiveText
        {
            get
            {

                var Protectivenames = OccHisProtectives?.Select(o => o.Text).ToList();
                if (Protectivenames != null && Protectivenames.Count > 0)
                {
                    return string.Join(",", Protectivenames);

                }
                else
                {
                    return "";

                }


            }

        }

#if Application
        [IgnoreMap]
#endif
        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual string HisHazardFactorsText
        {
            get
            {
                var Hurtivenames = OccHisHazardFactors?.Select(o => o.Text).ToList();
                if (Hurtivenames != null && Hurtivenames.Count > 0)
                {
                    return string.Join(",", Hurtivenames);

                }
                else
                {
                    return "";

                }


            }
        }





    }
}
