using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisProposalNew.Dto;
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
    [AutoMap(typeof(TjlOccQuesMerriyHistory))]
#endif
    public class OccQuesMerriyHistoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>    
        public virtual Guid? CustomerRegBMId { get; set; }


      

        /// <summary>
        /// 婚姻日期
        /// </summary>
        public virtual DateTime? StarTime { get; set; }
        /// <summary>
        /// 放射线种类ID
        /// </summary>
        [StringLength(640)]
        public virtual string TbmOccDictionaryIDs { get; set; }

        /// <summary>
        /// 放射线种类ID
        /// </summary>
        [StringLength(640)]
        public virtual string Radioactive { get; set; }

        ///// <summary>
        ///// 放射线种类
        ///// </summary>
        //public virtual ICollection<ShowOccDictionary> TbmOccDictionarys { get; set; }
        /// <summary>
        /// 职业及健康状况
        /// </summary>
        [StringLength(640)]
        public virtual string OccHealth { get; set; }
    }
}
