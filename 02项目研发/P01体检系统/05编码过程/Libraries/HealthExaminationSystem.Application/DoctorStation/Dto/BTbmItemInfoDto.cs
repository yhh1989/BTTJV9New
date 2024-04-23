using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    /// <summary>
    /// 项目设置
    /// 查询项目字典表
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmItemInfo))]
#endif
    public class BTbmItemInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 是否进入小结 1进入2不进入
        /// </summary>
        public virtual int? IsSummary { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(32)]
        public virtual string Unit { get; set; }
        /// <summary>
        /// 项目类别 1数值型2计算型3说明型4阴阳性
        /// </summary>
        public virtual int? moneyType { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string ItemBM { get; set; }
    }
}
