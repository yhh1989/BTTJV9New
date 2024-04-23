using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmBarSettings))]
#endif
    public class SimpleBarSettingDto : EntityDto<Guid>
    {
        /// <summary>
        ///     条码名称
        /// </summary>
        [StringLength(32)]
        public virtual string BarName { get; set; }

        /// <summary>
        ///     条码序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

    }
}
