using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Scheduling;

namespace Sw.Hospital.HealthExaminationSystem.Core.Coding
{
    /// <summary>
    /// 科室设置
    /// </summary>
    public class TbmDepartment : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// 组合集合
        /// </summary>
        public virtual ICollection<TbmItemGroup> ItemGroups { get; set; }

        /// <summary>
        /// 项目集合
        /// </summary>
        public virtual ICollection<TbmItemInfo> ItemInfos { get; set; }

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
        /// 部门类别 检查 检验 功能 放射 彩超 其他 耗材
        /// </summary>
        [StringLength(64)]
        public virtual string Category { get; set; }

        /// <summary>
        /// 部门职责
        /// </summary>
        [StringLength(64)]
        public virtual string Duty { get; set; }

        /// <summary>
        /// 性别 1男2女3不限
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? OrderNum { get; set; }

        /// <summary>
        /// 最大日检人数
        /// </summary>
        public virtual int? MaxCheckDay { get; set; }

        /// <summary>
        /// 贵宾科室地址
        /// </summary>
        [StringLength(512)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 女科室地址
        /// </summary>
        [StringLength(512)]
        public virtual string WomenAddress { get; set; }

        /// <summary>
        /// 男科室地址
        /// </summary>
        [StringLength(512)]
        public virtual string MenAddress { get; set; }

        /// <summary>
        /// 小结格式
        /// </summary>
        [StringLength(512)]
        public virtual string SumFormat { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 大科室
        /// </summary>
        public int? LargeDepart { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <summary>
        /// 人工行程安排集合
        /// </summary>
        [InverseProperty(nameof(ManualScheduling.DepartmentCollection))]
        public virtual ICollection<ManualScheduling> ManualSchedulingCollection { get; set; }
    }
}