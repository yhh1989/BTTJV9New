using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmSumHide))]
#endif
    public class TbmSumHideDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检类别 字典
        /// </summary>
        [StringLength(16)]
        public virtual string ClientType { get; set; }
        /// <summary>
        /// 是否正常 1为正常 2为异常
        /// </summary>
        public virtual int? IsNormal { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        [StringLength(1024)]
        public virtual string SumWord { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(1024)]
        public virtual string HelpChar { get; set; }
    }
}
