using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Sw.Hospital.HealthExaminationSystem.Core.Coding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Examination
{
   public class TjlLisApply : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        /// <summary>
        /// 体检人预约主键
        /// </summary>
        [ForeignKey("CustomerRegBM")]
        public virtual Guid? CustomerRegBMId { get; set; }

        /// <summary>
        /// 体检人预约ID
        /// </summary>
        public virtual TjlCustomerReg CustomerRegBM { get; set; }


        /// <summary>
        /// 申请编号
        /// </summary>
        [StringLength(32)]
        public virtual string  ApplyNO { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [StringLength(32)]
        public virtual string STATUS { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [StringLength(32)]
        public virtual string STATUSNAME { get; set; }

        /// <summary>
        /// 组合编码
        /// </summary>       
        [ForeignKey("ItemGroup")]
        public virtual Guid? ItemGroupID { get; set; }
        /// <summary>
        /// 组合编码
        /// </summary>  

        public virtual TbmItemGroup ItemGroup { get; set; }
        /// <summary>
        /// 组合名称
        /// </summary>
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>     
        public virtual DateTime APPLY_TIME { get; set; }
        /// <summary>
        /// 申请医师姓名
        /// </summary>
        [StringLength(32)]
        public virtual string APPLY_OPERATOR_NAME { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(32)]
        public virtual string REMARK { get; set; }        

        /// <inheritdoc />
        public int TenantId { get; set; }
    }
}
