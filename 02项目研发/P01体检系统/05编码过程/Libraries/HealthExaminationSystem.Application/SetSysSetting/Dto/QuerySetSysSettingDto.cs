using Abp.Application.Services.Dto;


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
namespace Sw.Hospital.HealthExaminationSystem.Application.SetSysSetting.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmBaritem))]
#endif
    public class QuerySetSysSettingDto : EntityDto<Guid>
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public virtual string KeyID { get; set; }
        /// <summary>
        /// 参数明细名称
        /// </summary>
        [StringLength(64)]
        public virtual string KeyName { get; set; }
    }
}
