using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.SchedulingSecondEdition.Dto
{
    /// <summary>
    /// 人工行程安排数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.Scheduling.ManualScheduling))]
#endif
    public class ManualSchedulingDtoNo1 : EntityDto<Guid>
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
        /// 单位
        /// </summary>
        public virtual CompanyInformationDtoNo1 Company { get; set; }

        /// <summary>
        /// 科室集合
        /// </summary>
        public virtual List<DepartmentDtoNo1> DepartmentCollection { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
#if Application
        [AutoMapper.IgnoreMap]
#endif
        public List<string> Description { get; set; }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            if (CompanyId.HasValue)
            {
                if (Company != null)
                {
                    if (string.IsNullOrWhiteSpace(Company.ClientAbbreviation))
                    {
                        return Company.ClientName;
                    }
                    else
                    {
                        return Company.ClientAbbreviation;
                    }
                }
            }
            else
            {
                return Name;
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取说明
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            if (DepartmentCollection != null && DepartmentCollection.Count != 0)
            {
                var nameList = DepartmentCollection.Select(r => r.Name).ToList();
                nameList.Insert(0,"Title涉及科室");
                return string.Join("|", nameList);
            }

            if (Description != null && Description.Count != 0)
            {
                return string.Join("|", Description);
            }

            return null;
        }
    }
}