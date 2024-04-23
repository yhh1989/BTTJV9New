using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
  public   class SimBasicDictionariesDto
    {
        /// <summary>
        /// 值
        /// </summary>
        public virtual int Value { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        [StringLength(64)]
        public virtual string Text { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(3072)]
        public virtual string Remarks { get; set; }
    }
}
