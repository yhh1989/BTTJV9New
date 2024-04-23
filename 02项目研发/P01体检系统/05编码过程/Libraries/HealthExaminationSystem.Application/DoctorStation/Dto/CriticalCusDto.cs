using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
   public  class CriticalCusDto : EntityDto<Guid>
    {
        
        public virtual string 体检号 { get; set; }
        public virtual string 姓名 { get; set; }
   
        public virtual DateTime? 登记时间 { get; set; }
        public virtual int? 状态 { get; set; }
        public virtual List<CriticalItemDto> 危急值结果 { get; set; }
}
}
