#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionSuspected.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlOccCustomerSum))]
#endif
    public class OccConclusionSuspectedShow
    {

        public virtual System.Guid? CustomerRegBMId { get; set; }
        /// <summary>
        /// 职业健康结论
        /// </summary>
        public virtual string Conclusion { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }
    
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public virtual string TypeWork { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string IDCardNo { get; set; }
        /// <summary>
        /// 危害因素 多个逗号拼接
        /// </summary>
        public virtual string ZYRiskName { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public virtual string ZYTreatmentAdvice { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>

        public virtual string PostState { get; set; }


        /// <summary>
        /// 处理意见
        /// </summary>
        public virtual string Opinions { get; set; }



        

    }
}
