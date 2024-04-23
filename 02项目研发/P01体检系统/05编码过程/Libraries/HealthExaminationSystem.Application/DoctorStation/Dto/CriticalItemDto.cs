using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
    public  class CriticalItemDto : EntityDto<Guid>
    {
        public virtual Guid? ItemID { get; set; }
        public virtual int? 分类 { get; set; }
        public virtual string 项目名称 { get; set; }
        public virtual string 结果 { get; set; }
    }
}
