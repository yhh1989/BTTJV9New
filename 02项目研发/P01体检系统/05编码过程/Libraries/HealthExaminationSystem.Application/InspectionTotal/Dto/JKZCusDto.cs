using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
  public   class JKZCusDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
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
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>      
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 总检时间
        /// </summary>      
        public virtual DateTime? SumDate { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(32)]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>   
        public virtual string ClientName { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>     
        public virtual string Qualified { get; set; }

        /// <summary>
        /// 原因
        /// </summary>     
        public virtual string Opinion { get; set; }
        
    }
}
