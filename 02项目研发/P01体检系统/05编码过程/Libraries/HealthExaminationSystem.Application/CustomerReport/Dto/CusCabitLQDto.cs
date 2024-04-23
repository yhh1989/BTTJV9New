using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlCusCabit))]
#endif
    public class CusCabitLQDto : EntityDto<Guid>
    {
              
       
        /// <summary>
        /// 取出状态1存入2取出
        /// </summary>
        public int? GetState { get; set; }
       

        /// <summary>
        /// 发单时间
        /// </summary>
        public virtual DateTime? SendTime { get; set; }


        /// <summary>
        /// 发单人Id
        /// </summary>    
        public virtual long? SenderId { get; set; }
       

        /// <summary>
        /// 领取人
        /// </summary>
        [StringLength(32)]
        public virtual string Receiver { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(256)]
        public virtual string Remarks { get; set; }
    }
}
