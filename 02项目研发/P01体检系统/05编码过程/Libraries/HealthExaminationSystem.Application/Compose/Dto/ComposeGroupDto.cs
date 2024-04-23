using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using HealthExaminationSystem.Enumerations.Helpers;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#elif Proxy
using Newtonsoft.Json;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Compose.Dto
{
    /// <summary>
    /// 组单分组
    /// </summary>
#if Application
    [AutoMapFrom(typeof(TbmComposeGroup))]
#endif
    public class ComposeGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        [StringLength(256)]
        public virtual string TeamName { get; set; }

        /// <summary>
        /// 适用性别
        /// </summary>
        public virtual int Sex { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int MinAge { get; set; }

        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int MaxAge { get; set; }

        /// <summary>
        /// 结婚状态
        /// </summary>
        public virtual int MaritalStatus { get; set; }

        /// <summary>
        /// 是否备孕 1备孕2不备孕
        /// </summary>
        public virtual int? ConceiveStatus { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        #region 格式化
#if Proxy

        /// <summary>
        /// 性别格式化
        /// </summary>
        [JsonIgnore]
        public virtual string FormatSex
        {
            get
            {
                if (Sex == 0)
                    return null;

                return SexHelper.GetSexModelsForItemInfo().Find(o => o.Id == Sex).Display;
            }
        }

        /// <summary>
        /// 婚姻格式化
        /// </summary>
        [JsonIgnore]
        public virtual string FormatMarital
        {
            get
            {
                if (MaritalStatus == 0)
                    return null;

                return MarrySateHelper.GetMarrySateModelsForItemInfo().Find(o => o.Id == MaritalStatus)
                    .Display;
            }
        }
#endif
        #endregion
    }
}