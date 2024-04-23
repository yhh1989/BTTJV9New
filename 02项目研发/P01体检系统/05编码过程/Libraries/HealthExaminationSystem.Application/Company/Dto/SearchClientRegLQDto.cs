using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
   public  class SearchClientRegLQDto : EntityDto<Guid>
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? StartCheckDate { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public virtual DateTime? EndCheckDate { get; set; }
    

        /// <summary>
        /// 柜子号
        /// </summary>
        [StringLength(32)]
        public virtual string CusCabitBM { get; set; }

        /// <summary>
        /// 存入状态
        /// </summary>        
        public virtual int? CusCabitState { get; set; }
        /// <summary>
        /// 存入时间
        /// </summary>        
        public virtual DateTime? StarCusCabitTime { get; set; }
        /// <summary>
        /// 存入时间
        /// </summary>        
        public virtual DateTime? EndCusCabitTime { get; set; }
    }
}
