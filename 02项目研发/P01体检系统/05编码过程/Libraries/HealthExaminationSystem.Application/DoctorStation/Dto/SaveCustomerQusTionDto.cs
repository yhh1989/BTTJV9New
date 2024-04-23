using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto
{
   public  class SaveCustomerQusTionDto : EntityDto<Guid>
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>  
        public virtual Guid? CustomerRegId { get; set; }
        /// <summary>
        /// 单位预约信息标识
        /// </summary>       
        public virtual Guid? ClientRegId { get; set; }
        /// <summary>
        /// 问卷标示
        /// </summary>
        public virtual Guid? OneAddXQuestionnaireid { get; set; }
        /// <summary>
        /// 问卷外部ID
        /// </summary>
        public virtual string OutQuestionID { get; set; }
        ///// <summary>
        ///// 问卷问题
        ///// </summary>
        //[StringLength(500)]
        //public virtual string QuestionName { get; set; }
        ///// <summary>
        ///// 问卷类别
        ///// </summary>
        //[StringLength(200)]
        //public virtual string QuestionType { get; set; }
    }
}
