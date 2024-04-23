using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if Application
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
   
    public class VerificationUserDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }
    }
}
