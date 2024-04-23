#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{

#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerOCCPic))]
#endif
    public class CustomerOCCPicDto
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public virtual Guid? TjlCustomerRegID { get; set; }       

        /// <summary>
        /// 图片ID
        /// </summary>  
        public virtual Guid? PictureBM { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }
    }
}
