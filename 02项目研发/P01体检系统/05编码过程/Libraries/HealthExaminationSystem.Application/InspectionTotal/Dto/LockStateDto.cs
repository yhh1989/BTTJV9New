using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
 public   class LockStateDto : EntityDto<Guid>
    {

        /// <summary>
        /// 总检锁定 1锁定2未锁定3解除所有锁定
        /// </summary>
        public virtual int? SummLocked { get; set; }


 

        /// <summary>
        /// 总检锁定用户ID
        /// </summary>     
        public virtual long? SummLockEmployeeBMId { get; set; }
    }
}
