using Abp.Application.Services.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Company.Dto
{
#if !Proxy
    [AutoMap(typeof(TjlShortMessage))]
#endif
    public class ShortMessageDto : EntityDto<Guid>
    {

        /// <summary>
        /// 预约外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        public virtual Guid CustomerRegId { get; set; }

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
        /// 移动电话
        /// </summary>
        [StringLength(16)]
        public virtual string Mobile { get; set; }


        /// <summary>
        /// 移动电话
        /// </summary>
        [StringLength(255)]
        public virtual string Message { get; set; }


        /// <summary>
        /// 0未发送1已发送2已接收
        /// </summary>    
        public virtual int SendState { get; set; }

        /// <summary>
        /// 发送日期
        /// </summary>      
        public virtual DateTime? SendTime { get; set; }

        /// <summary>
        /// 短信类别1预约短信2报告发送短信3复查4危急值5补检
        /// </summary>      
        public virtual int MessageType { get; set; }
    }
}
