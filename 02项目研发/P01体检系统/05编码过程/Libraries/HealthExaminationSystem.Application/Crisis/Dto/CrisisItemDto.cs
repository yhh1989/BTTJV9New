using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    public class CrisisItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 项目名称
        /// </summary>   
        public virtual string ItemName { get; set; }
        /// <summary>
        /// 项目结果，中文说明 数值型也存入
        /// </summary>
        [StringLength(3072)]
        public virtual string ItemResultChar { get; set; }

        /// <summary>
        /// 第一次检查时间 bxy
        /// </summary>
        public virtual DateTime? FirstDateTime { get; set; }
        /// <summary>
        /// 上报人
        /// </summary>    
        public virtual long? CheckEmployeeBMId { get; set; }


        /// <summary>
        /// 危急值回访状态 0未上报1已上报2已取消3已审核
        /// </summary>
        public virtual int? CrisisVisitSate { get; set; }

        /// <summary>
        /// 危急值等级
        /// </summary>
        public virtual int? CrisisLever { get; set; }
        /// <summary>
        /// 危急值提示
        /// </summary>
        public virtual string CrisiChar { get; set; }
        /// <summary>
        /// 重要异常说明
        /// </summary>
        public virtual string CrisiContent { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public virtual DateTime? ExamineTime { get; set; }
        /// <summary>
        /// 审核人
        /// </summary> 
        public virtual long? ExamineUserId { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public virtual DateTime? MessageTime { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>
        public virtual long? MessageUserId { get; set; }

        /// <summary>
        /// 通知状态1未通知2已通知
        /// </summary>
        public virtual int? MessageState { get; set; }
        /// <summary>
        /// 超时
        /// </summary>
        public virtual int? Isovertime { get; set; }

        /// <summary>
        /// 危急值短信 0未发送 1已发送
        /// </summary>
        public int? CrissMessageState { get; set; }

        /// <summary>
        /// 项目单位
        /// </summary>      
        public virtual string Unit { get; set; }
    }
}
