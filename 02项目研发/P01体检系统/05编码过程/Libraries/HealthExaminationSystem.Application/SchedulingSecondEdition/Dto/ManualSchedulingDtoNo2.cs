using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition.Dto
{
    /// <summary>
    /// 人工行程安排数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Scheduling.ManualScheduling))]
#endif
    public class ManualSchedulingDtoNo2 : EntityDto<Guid>
    {
        /// <summary>
        /// 个人排期的名称
        /// </summary>
        [StringLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 体检人数
        /// </summary>
        public int NumberOfCustomer { get; set; }

        /// <summary>
        /// 排期日期
        /// </summary>
        public DateTime SchedulingDate { get; set; }

        /// <summary>
        /// 单位标识
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// 科室集合
        /// </summary>
        public List<Guid> DepartmentIdList { get; set; }
    }
}