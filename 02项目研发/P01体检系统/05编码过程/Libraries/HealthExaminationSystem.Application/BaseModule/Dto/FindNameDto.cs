using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#if !Proxy
using Abp.Runtime.Validation; 
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.BaseModule.Dto
{
    public class FindNameDto
#if !Proxy
        : ICustomValidate 
#endif
    {
        [Required]
        public List<string> Names { get; set; }

#if !Proxy
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (Names.Count == 0)
                context.Results.Add(new ValidationResult($"{nameof(Names)}的长度不能为零！"));
        } 
#endif
    }
}