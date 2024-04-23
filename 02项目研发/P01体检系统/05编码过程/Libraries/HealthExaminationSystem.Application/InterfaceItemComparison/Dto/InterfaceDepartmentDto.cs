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
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class InterfaceDepartmentDto : EntityDto<Guid>
    {
        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string DepartmentBM { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>    
        [StringLength(64)]
        public virtual string Name { get; set; }

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
