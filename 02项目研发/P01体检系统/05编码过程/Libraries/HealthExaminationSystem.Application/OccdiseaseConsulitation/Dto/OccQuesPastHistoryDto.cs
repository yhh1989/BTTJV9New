using Abp.Application.Services.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlOccQuesPastHistory))]
#endif
    public class OccQuesPastHistoryDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
        public virtual Guid? CustomerRegBMId { get; set; }
        /// <summary>
        /// 疾病名称
        /// </summary>

        public virtual string IllName { get; set; }

        /// <summary>
        /// 其他疾病名称
        /// </summary>     
        public virtual string OtherIllName { get; set; }
        /// <summary>
        /// 诊断日期
        /// </summary>

        public virtual DateTime? DiagnTime { get; set; }
        /// <summary>
        /// 诊断单位
        /// </summary>
      
        public virtual string DiagnosisClient { get; set; }
        /// <summary>
        /// 治疗方式
        /// </summary>
       
        public virtual string Treatment { get; set; }
        /// <summary>
        /// 是否痊愈
        /// </summary>

        public virtual int? Iscured { get; set; }

        /// <summary>
        /// 诊断证书编号
        /// </summary>      
        public virtual string DiagnosticCode { get; set; }

    }
}
