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
    /// 答卷题目详情
    /// </summary>
    public class TjlQuestionBom : AuditedEntity<Guid>, IMustHaveTenant
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
        /// 题目
        /// </summary>
        [StringLength(320)]
        public virtual string bomItemName { get; set; }
        /// <summary>
        /// 题目类型1:填空;2:单选;3: 复选
        /// </summary>     
        public virtual int? bomItemType { get; set; }
        /// <summary>
        /// 是否必做1:是;0:否
        /// </summary>   
        public virtual int? mustFill { get; set; }
        /// <summary>
        /// 序号
        /// </summary>   
        public virtual int? OrderNum { get; set; }


        /// <summary>
        /// 题目
        /// </summary>
        [StringLength(320)]
        public virtual string Title { get; set; }
        /// <summary>
        /// 填空题答案
        /// </summary>
        [StringLength(320)]
        public virtual string answerContent { get; set; }

        /// <summary>
        /// 选项题备选项
        /// </summary>

        public virtual List<TjlQuestiontem> itemList { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
