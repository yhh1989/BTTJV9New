using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto
{
   public class ConfiStrDto
    {
        /// <summary>
        /// 提示内容
        /// </summary>
        [StringLength(64)]
        public virtual string StrTS { get; set; }
    }
}
