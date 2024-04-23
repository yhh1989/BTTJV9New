using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{    /// <summary>
     /// 窗体创建用户对象
     /// </summary>
#if !Proxy
    [AutoMapTo(typeof(User))]
#endif
    public class CreateUserFormTeandIDDto
    {

        [Required]
#if !Proxy
        [StringLength(AbpUserBase.MaxUserNameLength)]
#endif
        public string UserName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(32)]
        public virtual string EmployeeNum { get; set; }

        [Required]
#if !Proxy
        [StringLength(AbpUserBase.MaxNameLength)]
#endif
        public string Name { get; set; }

        [Required]
#if !Proxy
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
#endif
        public string Password { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(32)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        [StringLength(32)]
        public virtual string PhoneNumber { get; set; }

        public virtual List<Guid> FormRoleIds { get; set; }

        [Required]
#if !Proxy
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
#endif
        public string EmailAddress { get; set; }

        public virtual List<Guid> DepartmentIds { get; set; }

        /// <summary>
        /// 职位状态
        /// </summary>
        public virtual int? State { get; set; }

        /// <summary>
        /// Windows Doamin 域用户帐号, 多个帐号用逗号分开
        /// </summary>
        [StringLength(64)]
        public virtual string DomainName { get; set; }

        /// <summary>
        /// 员工职能
        /// </summary>
        [StringLength(32)]
        public virtual string Duty { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [StringLength(32)]
        public virtual string Address { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public virtual string Discount { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public virtual Guid? SignImage { get; set; }
        /// <summary>
        /// 租户
        /// </summary>
        public int TenantId { get; set; }
    }
}
