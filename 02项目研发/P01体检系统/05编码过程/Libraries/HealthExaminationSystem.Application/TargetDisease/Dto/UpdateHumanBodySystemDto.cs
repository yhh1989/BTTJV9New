using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.TargetDisease.Dto
{
#if !Proxy
    [AutoMap(typeof(Core.Illness.HumanBodySystem))]
#endif
    public class UpdateHumanBodySystemDto : EntityDto<Guid>
    {
        /// <inheritdoc />
        public bool IsActive { get; set; }

        /// <summary>
        /// 体格检查名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public string MnemonicCode { get; set; }
    }
}
