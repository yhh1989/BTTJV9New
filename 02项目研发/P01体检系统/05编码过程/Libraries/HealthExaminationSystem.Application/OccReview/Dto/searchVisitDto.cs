using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto
{
  public  class searchVisitDto
    {
        /// <summary>
        /// 档案号
        /// </summary> 
        public virtual string ArchivesNum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>    
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>       
        public virtual string IDCardNo { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>     
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 最小年龄
        /// </summary>
        public virtual int? MinAge { get; set; }
        /// <summary>
        /// 最大年龄
        /// </summary>
        public virtual int? MaxAge { get; set; }
        /// <summary>
        /// 登记日期 
        /// </summary>
        public virtual DateTime? LoginDateStar { get; set; }
        /// <summary>
        /// 结束日期 
        /// </summary>
        public virtual DateTime? LoginDateEnd { get; set; }
        /// <summary>
        /// 复查开始日期 
        /// </summary>
        public virtual DateTime? ReDateStar { get; set; }
        /// <summary>
        /// 复查结束日期 
        /// </summary>
        public virtual DateTime? ReDateEnd { get; set; }

        /// <summary>
        /// 补检开始日期 
        /// </summary>
        public virtual DateTime? BDateStar { get; set; }
        /// <summary>
        /// 复查结束日期 
        /// </summary>
        public virtual DateTime? BDateEnd { get; set; }
        /// <summary>
        /// 回访日期
        /// </summary>
        public DateTime? VisitTimeStart { get; set; }
        /// <summary>
        /// 回访日期
        /// </summary>
        public DateTime? VisitTimeEnd { get; set; }

        /// <summary>
        ///  回访状态0未回访1已回访3已取消
        /// </summary>
        public virtual int? VisitSate { get; set; }
        /// <summary>
        /// 回访人外键
        /// </summary>      
        public virtual long? VisitEmployeeId { get; set; }

    }
}
