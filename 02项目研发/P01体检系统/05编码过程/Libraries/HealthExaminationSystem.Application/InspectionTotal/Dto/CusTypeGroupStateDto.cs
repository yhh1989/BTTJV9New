#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{    ///// <summary>
    ///// 体检人预约组合信息表
    ///// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class CusTypeGroupStateDto
    {
        /// <summary>
        /// 科室名称
        /// </summary> 
        public virtual string DepartmentName { get; set; }

        public virtual int? CheckState { get; set; }

        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        public virtual int? IsAddMinus { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public virtual DepartCategoryDto DepartmentBM { get; set; }
    }
}
