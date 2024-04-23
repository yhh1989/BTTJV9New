using Abp.Application.Services.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto
{ 
  public   class OutApplicationDto : EntityDto<Guid>
    {
        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplicationNum { get; set; }

        /// <summary>
        /// 折后价
        /// </summary>
        public decimal ZHMoney { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public int? SQSTATUS { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? CreatTime { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary> 
        [StringLength(500)]
        public virtual string FPName { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [StringLength(500)]
        public virtual string Remark { get; set; }
        /// <summary>
        /// 回款金额
        /// </summary>
        public decimal? REFYZK { get; set; }
        /// <summary>
        /// 最后回款日期
        /// </summary>
        public DateTime? RETIME { get; set; }
        /// <summary>
        /// 体检人数
        /// </summary>
#if !Proxy
#endif
        [JsonIgnore]
        public virtual string fSQSTATUS
        {
            get
            {
                if (SQSTATUS == 1)
                    return "未收费";
                else if(SQSTATUS == 2)
                    return "已收费";
                else if(SQSTATUS == 4)
                    return "部分收费";
                else 
                    return "已作废";
            }
        }




    }
}
