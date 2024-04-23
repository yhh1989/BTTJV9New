
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Occupational;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.OccHarmFul.Dto
{
#if !Proxy
    [AutoMapFrom(typeof(TjlOccCustomerSum))]
#endif
    public class OutOccFactoryDto
    {

        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual string PostState { get; set; }
        /// <summary>
        /// 父级名称
        /// </summary>
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// 危害因素名称
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 危害因素分类
        /// </summary>
        public string TextType { get; set; }
        /// <summary>
        /// 实检人数
        /// </summary>
        public int? CheckNumber { get; set; }

        /// <summary>
        /// 异常人数
        /// </summary>
        public int? AbnormalNumber { get; set; }

        /// <summary>
        /// 检出率
        /// </summary>
        public double recallNumber { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartCheckDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndCheckDate { get; set; }
        /// <summary>
        /// 年度
        /// </summary>
        public string YearTime { get; set; }

    }
}
