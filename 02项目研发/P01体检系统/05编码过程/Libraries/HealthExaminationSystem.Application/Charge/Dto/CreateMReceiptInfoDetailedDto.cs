
using Sw.Hospital.HealthExaminationSystem.Application.Department.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Payment;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto
{
    /// <summary>
    /// 结算明细表
    /// </summary>
#if !Proxy
    [AutoMapTo(typeof(TjlMReceiptDetails))]
#endif
    public class CreateMReceiptInfoDetailedDto
    {


        /// <summary>
        /// 结算ID
        /// </summary>       
        public virtual Guid MReceiptInfoID { get; set; }

        /// <summary>
        /// 组合ID
        /// </summary>      
        public virtual Guid ItemGroupId { get; set; }      

        /// <summary>
        /// 组合编码
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupCodeBM { get; set; }
        /// <summary>
        /// 项目组合名称
        /// </summary>
        [StringLength(32)]
        public virtual string ItemGroupName { get; set; }

        /// <summary>
        /// 组合序号
        /// </summary>
        public virtual int? ItemGroupOrder { get; set; }
        /// <summary>
        /// 科室ID
        /// </summary>      
        public virtual Guid DepartmentId { get; set; }
       
        /// <summary>
        /// 科室名称
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        [StringLength(32)]
        public virtual string DepartmentCodeBM { get; set; }
        /// <summary>
        /// 科室序号
        /// </summary>
        public virtual int? DepartmentOrder { get; set; }

        /// <summary>
        /// 收费分类
        /// </summary>
        [StringLength(64)]
        public virtual string  ReceiptTypeName { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal GroupsMoney { get; set; }

        /// <summary>
        /// 折扣后
        /// </summary>
        public virtual decimal GroupsDiscountMoney { get; set; }

        /// <summary>
        /// 平均折扣率
        /// </summary>
        public virtual decimal Discount { get; set; }


    }
}
