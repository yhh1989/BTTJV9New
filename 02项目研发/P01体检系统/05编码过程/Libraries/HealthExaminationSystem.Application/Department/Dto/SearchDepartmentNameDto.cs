using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.Runtime.Validation;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Department.Dto
{
    public class SearchDepartmentNameDto
#if !Proxy
        : ICustomValidate
#endif
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        [Required]
        [StringLength(64)]
        public virtual string Name { get; set; }

#if !Proxy
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (Name.Trim() == string.Empty)
            {
                context.Results.Add(new ValidationResult($"{nameof(Name)}不能为空字符串！"));
            }
        }
#endif
    }
}