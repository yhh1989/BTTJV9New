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
    /// 选项题备选项
    /// </summary>
    public class TjlQuestiontem : AuditedEntity<Guid>, IMustHaveTenant
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
        /// 体检人问卷标识
        /// </summary>
        [ForeignKey("CusQuestion")]
        public virtual Guid? CusQuestionId { get; set; }
        /// <summary>
        /// 体检人问卷
        /// </summary>
        public virtual TjlCusQuestion CusQuestion { get; set; }

        /// <summary>
        /// 答卷题目详情
        /// </summary>
        [ForeignKey("QuestionBom")]
        public virtual Guid? QuestionBomId { get; set; }
        /// <summary>
        /// 答卷题目详情
        /// </summary>
        public virtual TjlQuestionBom QuestionBom { get; set; }
        /// <summary>
        /// 选项
        /// </summary>
        [StringLength(320)]
        public virtual string itemName { get; set; }

        /// <summary>
        /// 给的分数
        /// </summary>   
        public virtual int? grade { get; set; }

        /// <summary>
        /// 是否已选中
        /// </summary>    
        public virtual int isSelected { get; set; }

        /// <summary>
        /// 序号
        /// </summary>   
        public virtual int? OrderNum { get; set; }
        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
