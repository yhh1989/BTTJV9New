using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup;
namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    public class CrisVisitSelectDto: EntityDto<Guid>
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 发生日期
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }
      
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 性别（）
        /// </summary>
        public string Sexs { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Moblie { get; set; }


        /// <summary>
        /// 项目Id外键
        /// </summary>
    
        public virtual Guid? ItemId { get; set; }


        /// <summary>
        /// 项目组合
        /// </summary>

        public virtual Guid? GroupId { get; set; }       

        /// <summary>
        /// 科室ID
        /// </summary>
        public Guid? DepartmentBM { get; set; }

    }
}
