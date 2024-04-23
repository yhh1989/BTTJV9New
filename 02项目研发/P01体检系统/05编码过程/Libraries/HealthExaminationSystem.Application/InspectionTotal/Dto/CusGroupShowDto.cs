using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
 
    public class CusGroupShowDto : EntityDto<Guid>
    {

        /// <summary>
        /// 项目组合ID
        /// </summary>      
        public virtual Guid? ItemGroupBM_Id { get; set; }


        /// <summary>
        /// 组合编码
        /// </summary>      
        public virtual string ItemGroupCodeBM { get; set; }

        /// <summary>
        /// 项目组合名称
        /// </summary>    
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 第一次检查时间 bxy
        /// </summary>
        public virtual DateTime? FirstDateTime { get; set; }

        /// <summary>
        /// 检查人bxy
        /// </summary>       
        public virtual string InspectEmployeeName { get; set; }

        /// <summary>
        /// 审核人bxy
        /// </summary>      
        public virtual string CheckEmployeeName { get; set; }

        /// <summary>
        /// 项目检查状态 1未检查2已检查3部分检查4放弃5待查
        /// </summary>
        public virtual int? CheckState { get; set; }

        public virtual int?  DepartOrder { get; set; }
        public virtual int? GroupOrder { get; set; }

    }
}
