using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
   public  class reportjson
    {
        /// <summary>
        /// 首页
        /// </summary>
        public virtual ReportCusDto reportitems2 { get; set; }
        /// <summary>
        /// 复查
        /// </summary>
        public virtual List<ReportReviewDto> ReportReviews { get; set; }

        /// <summary>
        /// 总检 健康建议
        /// </summary>
        public virtual ReportSuggestBear reportitems3 { get; set; }

        /// <summary>
        /// 一般检查
        /// </summary>
        public virtual List<reportitemInfo> reportitem1 { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        public virtual List<reportitemInfo> reportitem2 { get; set; }
        /// <summary>
        /// 检验报表
        /// </summary>
        public virtual List<reportitemInfo> reportitem3 { get; set; }
        /// <summary>
        /// 影像报表
        /// </summary>
        public virtual List<reportitemInfo> reportitem4 { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public virtual string Template { get; set; }

        /// <summary>
        /// 总检时间（用于对方最后一次保存成功数据的时间节点）
        /// </summary>
        public virtual DateTime? time { get; set; }


        /// <summary>
        /// 体检号（用于对方存储失败后再次返回给接口）
        /// </summary>
        public virtual string CustomerBM { get; set; }


        /// <summary>
        /// 返回状态0失败，1正常
        /// </summary>
        public virtual int? code  { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Mess { get; set; }

        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(16)]
        public virtual string ArchivesNum { get; set; }

        /// <summary>
        /// 订单号（先为空）
        /// </summary>
        public virtual string dingdan { get; set; }

    }
}
