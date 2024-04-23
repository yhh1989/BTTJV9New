using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
    /// <summary>
    /// 搜索用户数据传输对象
    /// </summary>
    public class SearchUserDto
    {
        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(32)]
        public virtual string EmployeeNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        public virtual Guid? FormRole { get; set; }
        /// <summary>
        /// 姓名或工号
        /// </summary>
        public virtual string NameOrEmployeeNum { get; set; }
        /// <summary>
        /// 租户名称
        /// </summary>
        public virtual string TenantName { get; set; }
    }
}