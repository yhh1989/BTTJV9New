using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
 
namespace Sw.Hospital.HealthExaminationSystem.Application.CrossTable.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCusGiveUp))]
#endif
    public class CusGiveUpDto : EntityDto<Guid>
    {
        /// <summary>
        /// 待查时间（下次检查的时间）
        /// </summary>
        public virtual DateTime? stayDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string remart { get; set; }
        /// <summary>
        /// 类型 1放弃 2待查
        /// </summary>
        public virtual int stayType { get; set; }
        /// <summary>
        /// 体检人预约项目组主键
        /// </summary>    
        public virtual Guid? CustomerItemGroupId { get; set; }
    }
}
