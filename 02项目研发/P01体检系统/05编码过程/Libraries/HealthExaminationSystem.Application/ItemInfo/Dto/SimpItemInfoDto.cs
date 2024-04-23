using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemInfo))]
#endif
    public class SimpItemInfoDto : EntityDto<Guid>
    { 
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 小结时不显示项目名称 1显示2不显示
        /// </summary>
        public virtual int? IsSummaryName { get; set; }
        /// <summary>
        /// 项目类别 1数值型2计算型3说明型4阴阳性
        /// </summary>
        public virtual int? moneyType { get; set; }
    }
}
