using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.BarSetting.Dto
{
    public class SearchBarSettingDto : EntityDto<Guid>
    {
        /// <summary>
        ///     条码名称
        /// </summary>
        [StringLength(32)]
        public virtual string BarName { get; set; }
        
        /// <summary>
        ///     试管颜色
        /// </summary>
        [StringLength(8)]
        public virtual string TubeColor { get; set; }

        /// <summary>
        ///     检验方式 1外送2内检
        /// </summary>
        public virtual int? testType { get; set; }

        /// <summary>
        ///     是否启用 1启用2停止
        /// </summary>
        public virtual int? IsRepeatItemBarcode { get; set; }

        /// <summary>
        ///     打印方式 1档案号2档案号累加3自定义累加
        /// </summary>
        public virtual int? BarNUM { get; set; }

        /// <summary>
        ///     打印位置 1前台打印2抽血站打印
        /// </summary>
        public virtual int? PrintAdress { get; set; }
        
    }
}
