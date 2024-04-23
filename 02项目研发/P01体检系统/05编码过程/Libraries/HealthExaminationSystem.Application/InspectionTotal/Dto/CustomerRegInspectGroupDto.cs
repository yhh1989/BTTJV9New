#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{ 
#if !Proxy
    [Abp.AutoMapper.AutoMap(typeof(TjlCustomerItemGroup))]
#endif
    public class CustomerRegInspectGroupDto : EntityDto<Guid>
    {

        /// <summary>
        /// 项目组合ID
        /// </summary>    
        public virtual Guid? ItemGroupBM_Id { get; set; }
        /// <summary>
        /// 预约外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid CustomerRegId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>      
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }

        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrderNum { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>       
        public virtual string ItemGroupName { get; set; }


        /// <summary>
        /// 组合小结
        /// </summary>
   
        public virtual string ItemGroupSum { get; set; }

        /// <summary>
        /// 组合诊断 bxy
        /// </summary>       
        public virtual string ItemGroupDiagnosis { get; set; }
        /// <summary>
        ///是否职业健康项目1职业健康项目2否
        /// </summary>
        public virtual int? IsZYB { get; set; }
       
 
    }
}
