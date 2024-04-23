using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
    /// <summary>
    /// 条码视图
    /// </summary>
#if !Proxy
    [AutoMapFrom(typeof(TbmBarSettings))]
#endif
    public class BarCodeViewDto : EntityDto<Guid>
    {
        /// <summary>
        /// 条码名称
        /// </summary>
        [StringLength(32)]
        public virtual string BarName { get; set; }

        /// <summary>
        /// 是否启用
        /// <para>1 启用</para>
        /// <para>2 停止</para>
        /// </summary>
        public virtual int? IsRepeatItemBarcode { get; set; }

        /// <summary>
        /// 打印方式 1档案号2档案号累加3自定义累加
        /// </summary>
        public virtual int? BarNUM { get; set; }

        /// <summary>
        /// 打印个数 1
        /// </summary>
        public virtual int? BarPage { get; set; }

        /// <summary>
        /// 试管颜色
        /// </summary>
        [StringLength(32)]
        public virtual string TubeColor { get; set; }

        /// <summary>
        /// 打印位置 1前台打印2抽血站打印
        /// </summary>
        public virtual int? PrintAdress { get; set; }

        /// <summary>
        /// 条码设置 组合项目集合
        /// </summary>
        public virtual List<BarItemViewDto> Baritems { get; set; }
    }
}