using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
   public class INCusItemPIcDto
    {      
        /// <summary>
        /// 
        /// </summary>
        public virtual List<string> itemImages { get; set; }

        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual Guid? TjlCustomerRegID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public virtual Guid? ItemBMID { get; set; }
        /// <summary>
        /// 项目组合ID
        /// </summary>
        public virtual Guid? CustomerItemGroupID { get; set; }

  
    }
}
