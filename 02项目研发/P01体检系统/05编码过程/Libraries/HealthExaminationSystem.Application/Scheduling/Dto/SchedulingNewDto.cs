using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;
#elif Proxy
using Newtonsoft.Json;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{
#if Application
    [AutoMap(typeof(TjlScheduling))]
#endif
    public class SchedulingNewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 是否团体
        /// </summary>
        public virtual bool IsTeam { get; set; }

        /// <summary>
        /// 排期日期
        /// </summary>
        public virtual DateTime ScheduleDate { get; set; }

        /// <summary>
        /// 总人数
        /// </summary>
        public virtual int TotalNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 单位信息标识
        /// </summary>
        public Guid? ClientInfoId { get; set; }

        /// <summary>
        /// 单位信息
        /// </summary>
        public virtual ClientInfoRegDto ClientInfo { get; set; }

        /// <summary>
        /// 介绍人
        /// </summary>
        public string Introducer { get; set; }

        /// <summary>
        /// 个人名称
        /// </summary>
        public string PersonalName { get; set; }

#if Proxy
        [JsonIgnore]
        public string Name
        {
            get
            {
                if (IsTeam)
                    return ClientInfo.ClientName;
                return PersonalName;
            }
        }

        [JsonIgnore]
        public int ItemGroupCount => ItemGroups.Count;
#endif

        /// <summary>
        /// 项目组合信息
        /// </summary>
        public virtual List<ItemGroupOfScheduleDto> ItemGroups { get; set; }

        [StringLength(8)]
        public string TimeFrame { get; set; }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            if (Id != Guid.Empty)
            {
                return Id.GetHashCode();
            }

            return base.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is ItemGroupOfScheduleDto division)
            {
                if (Id != Guid.Empty)
                {
                    return Id == division.Id;
                }
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// 比较两个对象是否相等
        /// </summary>
        /// <param name="a1">对象1</param>
        /// <param name="a2">对象2</param>
        /// <returns>相等则为 True，否则为 False。</returns>
        public static bool operator ==(SchedulingNewDto a1, SchedulingNewDto a2)
        {
            return a1?.Id == a2?.Id;
        }

        /// <summary>
        /// 比较两个对象是否不相等
        /// </summary>
        /// <param name="a1">对象1</param>
        /// <param name="a2">对象2</param>
        /// <returns>不相等则为 True，否则为 False。</returns>
        public static bool operator !=(SchedulingNewDto a1, SchedulingNewDto a2)
        {
            return !(a1 == a2);
        }
    }
}