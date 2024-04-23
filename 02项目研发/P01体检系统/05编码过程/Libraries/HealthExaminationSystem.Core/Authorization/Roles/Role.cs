using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : AbpRole<User>
    {
        /// <summary>
        /// 说明最大长度
        /// </summary>
        public const int MaxDescriptionLength = 5000;

        /// <summary>
        /// 角色
        /// </summary>
        public Role()
        {
        }

        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="tenantId">租户标识</param>
        /// <param name="displayName">显示名称</param>
        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {
        }

        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="tenantId">租户标识</param>
        /// <param name="name">名称</param>
        /// <param name="displayName">显示名称</param>
        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {
        }

        /// <summary>
        /// 说明
        /// </summary>
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
    }
}