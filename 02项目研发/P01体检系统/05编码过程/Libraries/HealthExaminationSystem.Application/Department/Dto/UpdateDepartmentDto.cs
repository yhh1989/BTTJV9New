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
    [AutoMapTo(typeof(TbmDepartment))]
#endif
    public class UpdateDepartmentDto : EntityDto<Guid>
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
        /// 性别 1男2女3不限
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 部门类别 检查 检验 功能 放射 彩超 其他
        /// </summary>
        [StringLength(64)]
        public virtual string Category { get; set; }

        /// <summary>
        /// 最大日检人数
        /// </summary>
        public virtual int? MaxCheckDay { get; set; }

        /// <summary>
        /// 部门职责
        /// </summary>
        [StringLength(64)]
        public virtual string Duty { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 男科室地址
        /// </summary>
        [StringLength(512)]
        public virtual string MenAddress { get; set; }

        /// <summary>
        /// 女科室地址
        /// </summary>
        [StringLength(512)]
        public virtual string WomenAddress { get; set; }

        /// <summary>
        /// 贵宾科室地址
        /// </summary>
        [StringLength(512)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }
        /// <summary>
        /// 小结格式
        /// </summary>
        [StringLength(512)]
        public virtual string SumFormat { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 大科室
        /// </summary>
        public int? LargeDepart { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}