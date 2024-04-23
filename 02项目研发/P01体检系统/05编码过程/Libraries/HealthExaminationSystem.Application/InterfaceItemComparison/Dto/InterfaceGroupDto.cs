using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InterfaceItemComparison.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmItemGroup))]
#endif
    public class InterfaceGroupDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室Id
        /// </summary>
        public virtual Guid DepartmentId { get; set; }

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
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
    }
}
