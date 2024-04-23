using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerRegister.Dto
{
    /// <summary>
    /// 用户数据传输对象
    /// </summary>
#if !Proxy
    [Abp.AutoMapper.AutoMapFrom(typeof(Core.Authorization.Users.User))]
#endif
    public class UserDtoNo1 : EntityDto<long>
    {
        /// <summary>
        /// Name of the user.
        /// </summary>
        [StringLength(64)]
        public virtual string Name { get; set; }

        /// <summary>
        /// 签名图像标识
        /// </summary>
        public virtual Guid? SignImage { get; set; }
    }
}