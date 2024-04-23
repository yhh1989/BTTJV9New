#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlOccCustomerSum))]
#endif
    public class DQQuery
    {
        /// <summary>
        /// 当前数据
        /// </summary>
        public virtual int? CurrentData { get; set; }
    }
    //查询显示dto
    public class OccConclusionStatisticsShowDto
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
        public virtual Guid? CustomerRegBMId { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string IdCarNo { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        public virtual string Advise { get; set; }
        /// <summary>
        /// 工龄
        /// </summary>
        public virtual string InjuryAge { get; set; }


        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 工种
        /// </summary>

        public virtual string TypeWork { get; set; }

        /// <summary>
        /// 导诊开始时间
        /// </summary>
        public virtual DateTime? NavigationStartTime { get; set; }

        /// <summary>
        /// 导诊结束时间
        /// </summary>
        public virtual DateTime? NavigationEndTime { get; set; }

    

        /// <summary>
        /// 岗位类别
        /// </summary>

        public virtual string PostState { get; set; }
        ///// <summary>
        ///// 体检人信息dto
        ///// </summary>
        //public virtual TjlCustomerDto Customer { get; set; }



        ///// <summary>
        ///// 体检预约表Dto
        ///// </summary>
        //public virtual TjlCustomerRegDto CustomerReg { get; set; }

        /// <summary>
        /// 职业健康结论
        /// </summary>

        public virtual string Conclusion { get; set; }
        /// <summary>
        /// 危害因素
        /// </summary>
        public virtual string RiskS { get; set; }

    }
}
