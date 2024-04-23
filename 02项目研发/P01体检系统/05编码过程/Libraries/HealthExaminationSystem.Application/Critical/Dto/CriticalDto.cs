using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Sw.Hospital.HealthExaminationSystem.Application.Critical.Dto
{
    /// <summary>
    /// 查询客户预约信息Dto 
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TbmCriticalSet))]
#endif
    public class CriticalDto : EntityDto<Guid>
    {
        /// <summary>
        /// 科室标识
        /// </summary>      
        public virtual Guid DepartmentId { get; set; }

        /// <summary>
        /// 项目标识
        /// </summary>      
        public virtual Guid ItemInfoId { get; set; }
        /// <summary>
        /// 重要异常结果分类
        /// </summary> 
        public virtual int CriticalType { get; set; }

        /// <summary>
        /// 判断类型
        /// </summary>            
        public virtual int CalculationType { get; set; }
        /// <summary>
        /// 运算符
        /// </summary>            
        public virtual int Operator { get; set; }

        /// <summary>
        /// 数值结果
        /// </summary>            
        public virtual decimal? ValueNum { get; set; }


        /// <summary>
        /// 诊断结果
        /// </summary>            
        [StringLength(640)]
        public virtual string ValueChar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>            
        public virtual int? Sex { get; set; }
        /// <summary>
        /// 往次
        /// </summary>           
        public virtual int? Old { get; set; }

        public virtual ICollection<CriticalDetailDto> CriticalDetails { get; set; }

    }
}
