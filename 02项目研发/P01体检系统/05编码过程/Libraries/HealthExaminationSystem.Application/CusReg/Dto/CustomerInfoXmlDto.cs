using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{
   public   class CustomerInfoXmlDto
    {
       
        [StringLength(5000)]
        public virtual string  Details { get; set; }
    }
}
