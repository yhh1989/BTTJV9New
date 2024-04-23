using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Regist.Dto
{
   public  class TbmRegsitDto : EntityDto<Guid>
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        [StringLength(200)]
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 机器码
        /// </summary>
        [StringLength(200)]
        public virtual string MachineCode { get; set; }
        /// <summary>
        /// 注册码
        /// </summary>
        [StringLength(2000)]
        public virtual string RegistCode { get; set; }
    }
}
