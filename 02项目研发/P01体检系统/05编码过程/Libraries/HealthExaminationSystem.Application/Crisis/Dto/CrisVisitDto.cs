using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    public class CrisVisitDto: FullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 发生日期
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }
        
        /// <summary>
        /// 体检号
        /// </summary>
        public string CustomerBM { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
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
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemName { get; set; }


        /// <summary>
        /// 项目结果，中文说明 数值型也存入
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemResultChar { get; set; }



        /// <summary>
        /// 处理内容
        /// </summary>
        public string CallBacKContent { get; set; }

        /// <summary>
        /// 处理人标识
        /// </summary>
        public virtual long? CallBacKUserId { get; set; }
        /// <summary>
        /// 审核内容
        /// </summary>
        [StringLength(3072)]
        public string SHContent { get; set; }

        /// <summary>
        /// 危急值状态2正常1危急值
        /// </summary>
        public virtual int? CrisisSate { get; set; }

        /// <summary>
        /// 参考值
        /// </summary>
        [StringLength(256)]
        public virtual string Stand { get; set; }

        /// <summary>
        /// 审核人标识
        /// </summary>
        public virtual long? SHUserId { get; set; }

        /// <summary>
        /// 危急值记录Id
        /// </summary>
        public Guid CustomerRegId { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        
        public virtual string DepartmentName{ get; set; }

        ///// <summary>
        ///// 科室ID
        ///// </summary>
        //public virtual TbmDepartment DepartmentBM { get; set; }








    }
}