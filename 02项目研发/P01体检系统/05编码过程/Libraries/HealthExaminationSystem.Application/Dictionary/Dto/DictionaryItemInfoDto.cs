using System;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Dictionary.Dto
{
#if Application
    [AutoMap(typeof(TbmItemInfo))]
#endif
    public class DictionaryItemInfoDto : EntityDto<Guid>
    {

        /// <summary>
        /// 科室
        /// </summary>
        public virtual DictionaryDepartmentDto Department { get; set; }


        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string Name { get; set; }


        /// <summary>
        /// 项目序号
        /// </summary>
        public virtual int? OrderNum { get; set; }


    }

}