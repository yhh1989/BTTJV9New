using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using Sw.Hospital.HealthExaminationSystem.Core.Company;
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
    /// 体检人问题
    /// </summary>
    public class TjlCustomerQuestion : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约标识
        /// </summary>
        [ForeignKey(nameof(TjlCustomerReg))]
        public virtual Guid? CustomerRegId { get; set; }
        /// <summary>
        /// 单位预约信息标识
        /// </summary>
        [ForeignKey(nameof(ClientReg))]
        public virtual Guid? ClientRegId { get; set; }

        /// <summary>
        /// 单位预约信息
        /// </summary>
        public virtual TjlClientReg ClientReg { get; set; }
        /// <summary>
        /// 体检人信息
        /// </summary>
        public virtual TjlCustomerReg TjlCustomerReg { get; set; }
        /// <summary>
        /// 问卷标示
        /// </summary>
        [ForeignKey(nameof(OneAddXQuestionnaire))]
        public virtual Guid? OneAddXQuestionnaireid { get; set; }
        /// <summary>
        /// 问卷
        /// </summary>

        public virtual TbmOneAddXQuestionnaire OneAddXQuestionnaire{ get; set; }
        /// <summary>
        /// 问卷外部ID
        /// </summary>
        public virtual string OutQuestionID { get; set; }
        /// <summary>
        /// 问卷问题
        /// </summary>
        [StringLength(500)]
        public virtual string QuestionName { get; set; }
        /// <summary>
        /// 问卷类别
        /// </summary>
        [StringLength(200)]
        public virtual string QuestionType { get; set; }

        /// <summary>
        /// 外检上传状态 0未上传 1已上传  
        /// </summary>
        public int? WJUploadState { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

    }
}
