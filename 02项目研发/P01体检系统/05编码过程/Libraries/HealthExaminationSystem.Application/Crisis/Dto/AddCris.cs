using Abp.Application.Services.Dto;
using System;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    public class AddCris : EntityDto<Guid>
    {
        /// <summary>
        /// 预约id
        /// </summary>
       
        public Guid CustomerRegId { get; set; }
        /// <summary>
        /// 处理内容
        /// </summary>      
        public string CallBacKContent { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime CallBacKDate { get; set; }


        /// <summary>
        /// 处理人标识
        /// </summary>
        public virtual long? CallBacKUserId { get; set; }

        /// <summary>
        /// 回访状态 处理1 审核2
        /// </summary>
        public int? CallBackState { get; set; }


    }
}