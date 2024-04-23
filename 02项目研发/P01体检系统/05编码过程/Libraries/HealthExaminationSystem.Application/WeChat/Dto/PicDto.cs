#if Application
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public class PicDto
    {
        /// <summary>
        /// 图片ID
        /// </summary>
        public virtual Guid? PictureBM { get; set; }
        /// <summary>
        /// 预约ID
        /// </summary>
        //public virtual TjlCustomerReg CustomerRegBM { get; set; }

        public virtual PictureDto pictures { get; set; }

        public virtual Guid? CustomerItemGroupID { get; set; }
    }
}
