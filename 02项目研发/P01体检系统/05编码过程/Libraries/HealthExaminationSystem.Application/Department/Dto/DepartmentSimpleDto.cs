using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Department.Dto
{
#if !Proxy
    [AutoMap(typeof(TbmDepartment))]
#endif
    public class DepartmentSimpleDto : EntityDto<Guid>
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
        [Required]
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(64)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(64)]
        public virtual string WBCode { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 部门类别 检查 检验 功能 放射 彩超 其他 耗材
        /// </summary>
        [StringLength(64)]
        public virtual string Category { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
