
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.Crisis.Dto
{
    /// <summary>
    /// 危急值提示
    /// </summary>
   public  class CrisisCustomerDto
    {
        /// <summary>
        /// 体检号
        /// TjlCustomerReg
        /// </summary>
        public string CustomerBM { get; set; }

        /// <summary>
        /// 姓名
        /// TjlCustomer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// TjlCustomer
        /// </summary>
        public virtual string Sexs { get; set; }

        /// <summary>
        /// 年龄
        /// TjlCustomer
        /// </summary>
        public virtual int? Age { get; set; }

        /// <summary>
        /// 体检项目、
        /// TjlCustomerItemReg
        /// </summary>
        public virtual string ItemName { get; set; }

        /// <summary>
        /// 危急值状态 1正常2危急值
        /// TjlCustomerRegItem
        /// </summary>
        public virtual int? CrisisSate { get; set; } 

        /// <summary>
        /// 项目结果，中文说明 数值型也存入
        /// TjlCustomerRegItem
        /// </summary>
        public virtual string ItemResultChar { get; set; }

        /// <summary>
        /// 参考值
        /// </summary>
        public virtual string Stand { get; set; }

        
        /// <summary>
        /// 危急值记录Id
        /// </summary>
       
        public Guid CustomerRegId { get; set; }


        public DateTime CallBacKDate { get; set; }

        /// <summary>
        /// 处理内容
        /// </summary>
        public string CallBacKContent { get; set; }

        /// <summary>
        /// 处理人标识
        /// </summary>
        public virtual long? CallBacKUserId { get; set; }
        /// <summary>
        /// 审核内容
        /// </summary>
        [StringLength(3072)]
        public string SHContent { get; set; }

        /// <summary>
        /// 审核人标识
        /// </summary>
        public virtual long? SHUserId { get; set; }

        /// <summary>
        /// 体检人检查项目组合id
        /// </summary>
        public virtual Guid? CustomerItemGroupBMid { get; set; }
        /// <summary>
        /// 体检人项目id
        /// </summary>
        public virtual Guid Id { get; set; } 
    }
}


   



 



