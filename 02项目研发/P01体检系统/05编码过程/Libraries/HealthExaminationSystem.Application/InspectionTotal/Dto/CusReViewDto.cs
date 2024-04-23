using Abp.Application.Services.Dto;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
    /// <summary>
    /// 体检人预约登记信息表
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCusReview))]
#endif
    public class CusReViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>     
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 复查阳性
        /// </summary>      
        public virtual Guid? SummarizeAdviceId { get; set; }

        /// <summary>
        /// 复查项目组合
        /// </summary>    
        public virtual ICollection<GroupCusGroupDto> ItemGroup { get; set; }

        /// <summary>
        /// 复查时间
        /// </summary>
        public int ReviewDay { get; set; }
        /// <summary>
        /// 复查日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 复查备注
        /// </summary>
        public string Remart { get; set; }
#if Application
        [IgnoreMap]
#endif
        [JsonIgnore]
        public virtual string ItemGroupNames
        {
            get
            {
                var groupnames = ItemGroup.Select(o => o.ItemGroupName).ToList();
                string grouname = string.Join(",", groupnames);
                return grouname.TrimEnd(',');
            }

        }



    }
}
