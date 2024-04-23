using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;
using Microsoft.AspNet.Identity;
using Sw.Hospital.HealthExaminationSystem.Core.AppSystem;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : AbpUser<User>
    {
        /// <summary>
        /// 组合对应
        /// </summary>
        public virtual ICollection<TdbInterfaceEmployeeComparison> InterfaceEmployeeComparison { get; set; }
        /// <summary>
        /// 默认密码
        /// </summary>
        public const string DefaultPassword = "123qwe";

        /// <summary>
        /// 所属科室
        /// </summary>
        public virtual ICollection<TbmDepartment> TbmDepartments { get; set; }

        /// <summary>
        /// 拥有窗体角色
        /// </summary>
        public virtual ICollection<FormRole> FormRoles { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(32)]
        public virtual string EmployeeNum { get; set; }

        /// <summary>
        /// Windows Doamin 域用户帐号, 多个帐号用逗号分开
        /// </summary>
        [StringLength(64)]
        public virtual string DomainName { get; set; }

        /// <summary>
        /// Novell 网用户帐号,多个帐号用逗号分
        /// </summary>
        [StringLength(64)]
        public virtual string NovellAccount { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        [Obsolete("暂停使用", true)]
        [NotMapped]
        public virtual string EmployeeName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(32)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 性别 1男2女3未知
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime? Birthday { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(32)]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [StringLength(32)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [NotMapped]
        [Obsolete("暂停使用", true)]
        public virtual string sign { get; set; }

        /// <summary>
        /// 签名图像标识
        /// </summary>
        public virtual Guid? SignImage { get; set; }

        /// <summary>
        /// 职位状态 1在岗2离岗
        /// </summary>
        /// <value>
        /// 1表示在岗
        /// 2表示离岗
        /// </value>
        public virtual int? State { get; set; }

        /// <summary>
        /// 员工级别 字典
        /// </summary>
        public virtual int? Degree { get; set; }

        /// <summary>
        /// 员工职能
        /// </summary>
        [StringLength(32)]
        public virtual string Duty { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(32)]
        [Obsolete("暂停使用", true)]
        [NotMapped]
        public virtual string Pwd { get; set; }

        /// <summary>
        /// 是否有收费权限
        /// </summary>
        public virtual bool CanPayment { get; set; }

        /// <summary>
        /// 医生头像
        /// </summary>
        public virtual string EmPhoto { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [StringLength(64)]
        public virtual string QQ { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [MaxLength(64)]
        [Obsolete("暂停使用", true)]
        [NotMapped]
        public virtual string Email { get; set; }

        /// <summary>
        /// 工作状态
        /// </summary>
        public virtual int WorkState { get; set; }

        /// <summary>
        /// 最后退出系统时间
        /// </summary>
        public virtual DateTime? LastClearTime { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public virtual bool OnlineState { get; set; }

        /// <summary>
        /// 折扣范围
        /// </summary>
        [StringLength(64)]
        public virtual string Discount { get; set; }

        /// <summary>
        /// 劳务标准
        /// </summary>
        [StringLength(128)]
        public virtual string ServiceStandard { get; set; }

        /// <summary>
        /// 职称
        /// </summary>
        [StringLength(256)]
        public virtual string Title { get; set; }

        /// <summary>
        /// 临床科室
        /// </summary>
        [StringLength(256)]
        public virtual string ClinicalDepartment { get; set; }

        /// <summary>
        /// 体检分组 -内务信息组
        /// </summary>
        [StringLength(256)]
        public virtual string GroupName { get; set; }


        /// <summary>
        /// 团体审批
        /// </summary>
        public virtual int? ClientZKS { get; set; }

        /// <summary>
        /// 所属院区
        /// </summary>
        public virtual int? HospitalArea { get; set; }

        /// <summary>
        /// 个性化配置
        /// </summary>
        [InverseProperty(nameof(PersonnelIndividuationConfig.User))]
        public virtual PersonnelIndividuationConfig IndividuationConfig { get; set; }

        /// <summary>
        /// 创建随机密码
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        /// <summary>
        /// 创建租户管理员用户
        /// </summary>
        /// <param name="tenantId">租户标识</param>
        /// <param name="emailAddress">邮件</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };

            return user;
        }
    }
}