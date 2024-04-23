#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.SummarizeAdvice.Dto
{
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TbmSumConflict))]
#endif
    public class SumConflictDto : EntityDto<Guid>
    {
        /// <summary>
        /// 关键词
        /// </summary>            
        [StringLength(640)]
        public virtual string SumWord { get; set; }
        /// <summary>
        /// 性别限制
        /// </summary>
        public virtual int? IsSex { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 年龄限制
        /// </summary>
        public virtual int? IsAge { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }
    }
}
