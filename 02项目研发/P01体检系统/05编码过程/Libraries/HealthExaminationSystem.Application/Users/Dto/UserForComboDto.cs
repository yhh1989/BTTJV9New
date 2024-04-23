using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
    /// <summary>
    /// 用户数据传输对象
    /// </summary>
#if !Proxy
    [AutoMap(typeof(User))]
#endif
    public class UserForComboDto : EntityDto<long>
    {
        /// <summary>
        /// 工号
        /// </summary>
        [StringLength(32)]
        public virtual string EmployeeNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        [StringLength(32)]
        public virtual string HelpChar { get; set; }

        /// <summary>
        /// 签名图像标识
        /// </summary>
        public virtual Guid? SignImage { get; set; }
    }
}