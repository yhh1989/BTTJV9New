using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto
{
  public   class OutCusListDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检号 
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>    
        public virtual Guid CustomerId { get; set; }
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
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string IDCardNo { get; set; }


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
        /// 报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>      
        public virtual DateTime? LoginDate { get; set; }


        /// <summary>
        /// 总检锁定 1锁定2未锁定
        /// </summary>
        public virtual int? SummLocked { get; set; }


        /// <summary>
        /// 总检锁定用户ID
        /// </summary>     
        public virtual long? SummLockEmployeeBMId { get; set; }
        /// <summary>
        /// 初审医生外键
        /// </summary>   
        public virtual long? CSEmployeeId { get; set; }       

        /// <summary>
        /// 复审医生外键
        /// </summary>
        public virtual long? FSEmployeeId { get; set; }


        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>   
        public virtual string ClientName { get; set; }
        /// <summary>
        /// 是否异常
        /// </summary>
        public virtual string ISIll { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>     
        [StringLength(128)]
        public virtual string FPNo { get; set; }

        /// <summary>
        /// 体检来源 字典ClientSource
        /// </summary>
        public virtual int? InfoSource { get; set; }


        /// <summary>
        /// 问卷状态 0已问卷 1未问卷
        /// </summary>
        public string forQuestionState { get; set; }


        /// <summary> 
        /// 问卷日期
        /// </summary>
        public DateTime? QuestionTime { get; set; }

      

    }
}
