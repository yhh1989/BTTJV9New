using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.Application.DynamicColumnDirectory.Dto
{
    /// <summary>
    /// 动态列配置数据传输对象
    /// </summary>
#if Application
    [Abp.AutoMapper.AutoMap(typeof(Core.DynamicColumnDirectory.DynamicColumnConfiguration))]
#endif
    public class DynamicColumnConfigurationDtoNo1 : EntityDto<Guid>
    {
        /// <summary>
        /// 表格视图标识
        /// </summary>
        [StringLength(64)]
        public string GridViewId { get; set; }

        /// <summary>
        /// 表格视图列名称
        /// </summary>
        [StringLength(64)]
        public string GridViewColumnName { get; set; }

        /// <summary>
        /// 可见的
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 可见的顺序值
        /// </summary>
        public int VisibleIndex { get; set; }

        /// <summary>
        /// 固定列
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        /// 左边固定
        /// </summary>
        public bool IsLeft { get; set; }

#if Proxy
        /// <summary>
        /// 表格视图列标题
        /// </summary>
        [JsonIgnore]
        public string GridViewColumnCaption { get; set; }

        /// <summary>
        /// 固定列格式化
        /// </summary>
        [JsonIgnore]
        public string FixedFormat => Fixed ? "固定" : string.Empty;

        /// <summary>
        /// 固定列格式化
        /// </summary>
        [JsonIgnore]
        public string IsLeftFormat => Fixed ? IsLeft ? "左" : "右" : string.Empty;
#endif
    }
}