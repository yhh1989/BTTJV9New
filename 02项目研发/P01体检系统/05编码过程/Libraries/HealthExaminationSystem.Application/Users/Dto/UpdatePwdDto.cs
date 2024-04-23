using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Users.Dto
{
    /// <summary>
    /// 修改密码Dto
    /// </summary>
    public class UpdatePwdDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// 原密码
        /// </summary>
        [Required]
        public virtual string Password { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        public virtual string NewPassword { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
#if Application
        [Compare("NewPassword", ErrorMessage = "密码和确认密码不匹配。")]
#endif
        public virtual string ConfirmPassword { get; set; }
    }
}