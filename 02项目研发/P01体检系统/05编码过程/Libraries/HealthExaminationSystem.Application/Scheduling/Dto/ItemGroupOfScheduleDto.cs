using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#elif Proxy
using Newtonsoft.Json;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Scheduling.Dto
{
#if Application
    [AutoMapFrom(typeof(TbmItemGroup))]
#endif
    public class ItemGroupOfScheduleDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(64)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 收费类别名称
        /// </summary>
        [StringLength(64)]
        public virtual string ChartName { get; set; }

#if Proxy
        [JsonIgnore]
        public int NumberOfPeople { get; set; }
#endif

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
        public static bool operator ==(ItemGroupOfScheduleDto a1, ItemGroupOfScheduleDto a2)
        {
            return a1?.Id == a2?.Id;
        }

        /// <summary>
        /// 比较两个对象是否不相等
        /// </summary>
        /// <param name="a1">对象1</param>
        /// <param name="a2">对象2</param>
        /// <returns>不相等则为 True，否则为 False。</returns>
        public static bool operator !=(ItemGroupOfScheduleDto a1, ItemGroupOfScheduleDto a2)
        {
            return !(a1 == a2);
        }
    }
}