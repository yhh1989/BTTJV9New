using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using System.Linq;
#if !Proxy
using AutoMapper;
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(User))]
#endif
    public class UserFormDto : EntityDto<long>
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserName { get; set; }

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
        /// 电话
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        [StringLength(32)]
        public virtual string Address { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual List<FormRoleDto> FormRoles { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public virtual ICollection<TbmDepartmentDto> TbmDepartments { get; set; }

#if Application
        [IgnoreMap]
#endif
        public virtual string FormRolesFormat
        {
            get
            {
                if (FormRoles == null)
                {
                    return "";
                }
                else 
                {
                    var roles = FormRoles.Select(p => p.Name).ToList();
                    var rolesname = string.Join(",", roles);
                    return rolesname;
                }
            }

        }
    }
}