using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
  public   class WCusRegDto
    {
      
        /// <summary> 
        /// 体检人项目组合
        /// </summary>
        public virtual List<WXCusGroupDto> CustomerItemGroup { get; set; }

        /// <summary>
        /// 体检人科室小结
        /// </summary>
        public virtual List<WCusDepSumDto> CustomerDepSummary { get; set; }

        /// <summary>
        /// 预约项目
        /// </summary>
        public virtual List<WCusRegItemDto> CustomerRegItems { get; set; }

        /// <summary>
        /// 总检建议
        /// </summary>
        public virtual WCusSumDto CustomerSummarize { get; set; }


        /// <summary>
        /// 档案号
        /// </summary>
        public virtual string ArchivesNum { get; set; }
        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string CustomerBM { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(32)]
        public virtual string OrderId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? LastTime { get; set; }

        /// <summary>
        /// 单位预约编码
        /// </summary>
        public virtual string ClientRegBM { get; set; }
        /// <summary>
        /// 分组编码
        /// </summary>
        public virtual int? ClientTeamInfoBM { get; set; }        

        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }

        /// <summary>
        /// 查询码
        /// </summary>
        [StringLength(32)]
        public virtual string WebQueryCode { get; set; }
       

        /// <summary>
        /// 预约日期
        /// </summary>
        public virtual DateTime? BookingDate { get; set; }

        /// <summary>
        /// 登记日期 第一次登记日期
        /// </summary>
        public virtual DateTime? LoginDate { get; set; }

        /// <summary>
        /// 婚姻状况 1未婚2结婚
        /// </summary>
        public virtual int? MarriageStatus { get; set; }

        /// <summary>
        /// 套餐编码
        /// </summary>
        public virtual string ItemSuitBM { get; set; }
      

        /// <summary>
        /// 登记状态 1未登记 2已登记
        /// </summary>
        public virtual int? RegisterState { get; set; }

        /// <summary>
        /// 体检状态 1未体检2体检中3体检完成
        /// </summary>
        public virtual int? CheckSate { get; set; }

        /// <summary>
        /// 总检状态 1未总检2已分诊3已初检4已审核
        /// </summary>
        public virtual int? SummSate { get; set; }

        /// <summary>
        /// 报告打印状态 1未打印2已打印
        /// </summary>
        public virtual int? PrintSate { get; set; }

        /// <summary>
        /// 是否健康管理 1管理2不管理
        /// </summary>
        public virtual int? MailingReport { get; set; }
      
        /// <summary>
        /// 收费状态 1未收费6已收费7欠费
        /// </summary>
        public virtual int? CostState { get; set; }

        /// <summary>
        /// 类别 1为普通用户2为VIP用户
        /// </summary>
        public virtual int? CustomerType { get; set; }

        /// <summary>
        /// 体检类别 1健康体检2职业健康体检3健康证体检4公务员体检5学生体检6驾驶证体检7婚检
        /// </summary>
        public virtual int? PhysicalType { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public virtual int? RegAge { get; set; }
        
    }
}
