using System;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
#if Application
    [AutoMap(typeof(StartupMenuBar))]
#endif
    public class StartupMenuBarDto : EntityDto<Guid>
    {
        /// <summary>
        /// 按钮项名称
        /// </summary>
        public string BarButtonItemName { get; set; }

        /// <summary>
        /// 按钮项标题
        /// </summary>
        public string BarButtonItemCaption { get; set; }
    }
}