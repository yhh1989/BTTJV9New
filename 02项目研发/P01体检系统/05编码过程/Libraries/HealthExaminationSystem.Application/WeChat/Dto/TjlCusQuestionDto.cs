#if !Proxy
using Abp.AutoMapper;
using Sw.Hospital.HealthExaminationSystem.Core.Examination;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.WeChat.Dto
{
    /// <summary>
    /// 用户预约组合
    /// </summary>
#if !Proxy
    [AutoMap(typeof(TjlCusQuestion))]
#endif
    public class TjlCusQuestionDto
    {
        

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(32)]
        public virtual string personName { get; set; }

        /// <summary>
        /// 体检号
        /// </summary>
        [StringLength(32)]
        public virtual string checkNo { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [StringLength(24)]
        public virtual string IDCardNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [StringLength(64)]
        public virtual string orderNo { get; set; }

        /// <summary>
        /// 微信openid
        /// </summary>
        [StringLength(32)]
        public virtual string openID { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(32)]
        public virtual string mobile { get; set; }

        /// <summary>
        /// 线上预约体检订单号
        /// </summary>
        [StringLength(32)]
        public virtual string tempPersonCheckOrderno { get; set; }

        /// <summary>
        /// 团队体检订单号
        /// </summary>
        [StringLength(32)]
        public virtual string teamCheckOrderno { get; set; }
        /// <summary>
        /// 问卷标题
        /// </summary>
        [StringLength(320)]
        public virtual string questionName { get; set; }
        /// <summary>
        /// 回答评定分数
        /// </summary>  
        public virtual int? answerGrade { get; set; }
        /// <summary>
        /// 是否已生成评估报告1:是;0:否
        /// </summary>     
        public virtual int? hasReport { get; set; }
        /// <summary>
        /// 评估报告文件地址
        /// </summary>
        [StringLength(32)]
        public virtual string reportUrl { get; set; }
        /// <summary>
        /// 回答评定结果
        /// </summary>
        [StringLength(32)]
        public virtual string evaluateResult { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [StringLength(32)]
        public virtual string lastTime { get; set; }

        /// <summary>
        /// 1自定义问卷2评估问卷
        /// </summary>     
        public virtual int? Type { get; set; }

        /// <summary>
        /// 答卷题目详情
        /// </summary>    
        public virtual List<TjlQuestionBomDto> questionBomList { get; set; }
    }
}
