using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
    public  class TjlCusMessage : AuditedEntity<Guid>, IMustHaveTenant
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
        /// 短信类别1=报告通知2=复查项目
        /// </summary>
        public virtual int? MesType { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
