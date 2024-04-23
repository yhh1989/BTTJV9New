using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{

    ///// <summary>
    ///// 体检人预约组合信息表
    ///// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class CusGroupStateDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>      
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary> 
        public virtual string DepartmentName { get; set; }

        /// <summary>
        /// 项目组合ID
        /// </summary>  
        public virtual Guid? ItemGroupBM_Id { get; set; }


        /// <summary>
        /// 项目组合名称
        /// </summary>  
        public virtual string ItemGroupName { get; set; }


        public virtual int? CheckState { get; set; }


        /// <summary>
        /// 加减状态 1为正常项目2为加项3为减项4调项减5调项加
        /// </summary>
        public virtual int? IsAddMinus { get; set; }
    }
}
