using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmBarSettings))]
#endif
    public class BarSettingDto : EntityDto<Guid>
    {
        /// <summary>
        /// 打印内容
        /// </summary>
        [StringLength(32)]
        public virtual string Content { get; set; }

        /// <summary>
        /// 标本类别 SpecimenType字典
        /// </summary>
        [StringLength(16)]
        public virtual string Sampletype { get; set; }

        /// <summary>
        /// 检验方式 1外送2内检
        /// </summary>
        public virtual int? testType { get; set; }

        /// <summary>
        /// 是否启用 1启用2停止
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
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 条码名称
        /// </summary>
        [StringLength(32)]
        public virtual string BarName { get; set; }

        /// <summary>
        /// 条码序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 试管颜色
        /// </summary>0     
        public virtual string StrBar { get; set; }
    }
}