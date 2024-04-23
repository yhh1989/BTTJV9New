using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    /// <summary>
    /// 问卷
    /// </summary>
    public class TjlCusQuestion : AuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>
        [ForeignKey("CustomerReg")]
        public virtual Guid? CustomerRegId { get; set; }

        /// <summary>
        /// 体检人预约
        /// </summary>
        public virtual TjlCustomerReg CustomerReg { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string personName { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string checkNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(64)]
        public virtual string orderNo { get; set; }

        /// <summary>
        /// 微信openid
        /// </summary>
        [StringLength(32)]
        public virtual string openID { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(32)]
        public virtual string mobile { get; set; }

        /// <summary>
        /// 线上预约体检订单号
        /// </summary>
        [StringLength(32)]
        public virtual string tempPersonCheckOrderno { get; set; }

        /// <summary>
        /// 团队体检订单号
        /// </summary>
        [StringLength(32)]
        public virtual string teamCheckOrderno { get; set; }

        /// <summary>
        /// 问卷标题
        /// </summary>
        [StringLength(32)]
        public virtual string questionName { get; set; }

        /// <summary>
        /// 回答评定分数
        /// </summary>
        public virtual int? answerGrade { get; set; }

        /// <summary>
        /// 是否已生成评估报告1:是;0:否
        /// </summary>
        public virtual int? hasReport { get; set; }

        /// <summary>
        /// 评估报告文件地址
        /// </summary>
        [StringLength(32)]
        public virtual string reportUrl { get; set; }

        /// <summary>
        /// 回答评定结果
        /// </summary>
        [StringLength(32)]
        public virtual string evaluateResult { get; set; }

#warning 标准的时间字段怎么就要以字符串存储呢
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [StringLength(32)]
        public virtual string lastTime { get; set; }

        /// <summary>
        /// 1自定义问卷2评估问卷
        /// </summary>
        public virtual int? Type { get; set; }

        /// <summary>
        /// 答卷题目详情
        /// </summary>
        public virtual List<TjlQuestionBom> questionBomList { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}