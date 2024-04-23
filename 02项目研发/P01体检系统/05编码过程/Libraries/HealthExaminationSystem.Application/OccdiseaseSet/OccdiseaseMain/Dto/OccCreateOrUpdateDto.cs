using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseSet.OccdiseaseMain.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmOccDisease))]
#endif
    public class OccCreateOrUpdateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 父级单位标识（如行业大类，症状大类）
        /// </summary>
      
        public virtual Guid? ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(500)]
        public virtual string Text { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>

        public virtual string HelpChar { get; set; }
        /// <summary>
        /// 是否启用1启用0停用
        /// </summary>   
        public virtual int IsActive { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public virtual string Remarks { get; set; }

    }
}
