using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 短信
    /// </summary>
    public class TjlShortMessage : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 预约外键---需要加上这个字段，linq查询时才可以判子表的条件，不然tolist时会报错。
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid CustomerRegId { get; set; }

        /// <summary>
        /// 预约id
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }

        ///// <summary>
        ///// 体检人外键
        ///// </summary>
        //[ForeignKey("Customer")]
        //public virtual Guid CustomerId { get; set; }

        ///// <summary>
        ///// 体检人
        ///// </summary>
        //public virtual TjlCustomer Customer { get; set; }

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
        /// 0为发送1已发送2已接收3发送失败4接收失败5补检
        /// </summary>    
        public virtual int SendState { get; set; }     

        /// <summary>
        /// 发送日期
        /// </summary>      
        public virtual DateTime? SendTime { get; set; }

        /// <summary>
        /// 短信类别1预约短信2报告发送短信3复查4危急值
        /// </summary>      
        public virtual int MessageType { get; set; }

        /// <summary>
        /// 发送状态码0成功，其他失败
        /// </summary>    
        [StringLength(64)]
        public virtual string Sendcode { get; set; }

        /// <summary>
        /// 状态码说明（成功返回空）
        /// </summary>    
        [StringLength(255)]
        public virtual string SenderrorMsg { get; set; }
        /// <summary>
        /// 消息id
        /// </summary>    
        [StringLength(255)]
        public virtual string msgId { get; set; }

        /// <summary>
        ///253平台收到运营商回复状态报告的时间，格式yyMMddHHmmss"notifyTime":"180522104730"
        /// </summary>    
        [StringLength(255)]
        public virtual string notifyTime { get; set; }
        /// <summary>
        /// 运营商返回的状态说明"statusDesc":"短信发送成功"
        /// </summary>    
        [StringLength(255)]
        public virtual string statusDesc { get; set; }

        /// <summary>
        ///状态更新时间，格式yyMMddHHmm，其中yy=年份的最后两位（00-99）
        /// </summary>    
        [StringLength(255)]
        public virtual string reportTime { get; set; }
        /// <summary>
        /// 运营商返回的状态（详细参考常见code.253.com常见状态报告状态码）"status":"DELIVRD"
        /// </summary>
        [StringLength(64)]
        public virtual string status { get; set; }
        /// <summary>
        /// 下发短信计费条数
        /// </summary>

        [StringLength(64)]
        public virtual string length { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
