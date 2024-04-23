using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    public class ChargeBM : EntityDto<Guid>
    {
       
        [StringLength(32)]
        public virtual string Name { get; set; }
    }
}